using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{

    #region FIELDS

    PlayerController controller;
    public GameObject eye;

    [Header("Walk")]
    public bool canMove = true;
    public float baseSpeed, maxNavRayStepDistance;
    public float extraSpeed;
    bool isMoving = false;

    [Header("Dash")]
    public bool canDash = true;
    public bool isDashing = false;
    public float maxNavRayDashDistance, dashSpeed, dashTimer;
    public LayerMask enemyLayer;
    Coroutine dashRoutine;

    private Vector2 currentMoveDir;
    private Collider2D col;
    private Animator anim;
    private Transform visualT;

    #endregion

    #region PROPERTIES

    public float CurrentSpeed
    {
        get
        {
            return extraSpeed + baseSpeed;
        }
    }

    public bool IsValidSituation()
    {
        if (!canMove || isDashing)
            return false;

        return true;
    }

    #endregion

    private void OnEnable()
    {
        if (anim == null)
            anim = GetComponentInChildren<Animator>();

        if (visualT == null)
            visualT = anim.transform;

        if (col == null)
            col = GetComponent<Collider2D>();

        if (controller == null)
            controller = GetComponent<PlayerController>();

        controller.actionReader.OnMoveEvent += TryMoveEvent;
        controller.actionReader.OnDashEvent += TryDashEvent;
    }

    private void OnDisable()
    {

        controller.actionReader.OnMoveEvent -= TryMoveEvent;
        controller.actionReader.OnDashEvent -= TryDashEvent;
    }

    public void TryMoveEvent(Vector3 direction, bool isPerforming)
    {
        direction.Normalize();
        currentMoveDir = direction;
    }

    // Update is called once per frame
    void Update()
    {

        ApplyMove();

        if (currentMoveDir.magnitude > 0.1f)
        {
            // Horizontal Flip
            if (currentMoveDir.x != 0)
                FlipVisuals(currentMoveDir.x);

            // Vertical Flip
            //if (currentMoveDir.y >= 0.1f)
            //{
            //    // Looking up
            //    eye.SetActive(false);
            //}
            //else
            //{
            //    // Looking down
            //    eye.SetActive(true);
            //}

        }
        else
        {
            if (anim.GetBool("isWalking"))
                anim.SetBool("isWalking", false);
        }


    }


    public void ApplyMove()
    {

        if (IsValidSituation())
        {
            isMoving = true;

            Vector2 step = (Vector2)transform.position + currentMoveDir * Time.deltaTime * CurrentSpeed;

            bool isValidStep = NavMesh.SamplePosition(step, out NavMeshHit hit, maxNavRayStepDistance, NavMesh.AllAreas);

            if (isValidStep)
            {
                if (currentMoveDir.magnitude >= 1)
                {
                    if ((transform.position - hit.position).magnitude >= .02f)
                    {
                        transform.position = (Vector2)hit.position;

                        if (!anim.GetBool("isWalking"))
                            anim.SetBool("isWalking", true);
                    }
                }

            }
            else
            {
                if (anim.GetBool("isWalking"))
                    anim.SetBool("isWalking", false);
            }
        }
    }

    #region DASH ABILITY

    private void TryDashEvent(bool isPerforming)
    {
        if (!isPerforming)
            return;

        if (canDash)
        {

            Vector2 step = (Vector2)transform.position + currentMoveDir * dashSpeed;
            
            /*

            //RaycastHit2D enemies = Physics2D.Raycast(transform.position, currentMoveDir, maxNavRayDashDistance *2f, enemyLayer);

            Collider2D enemies = Physics2D.OverlapBox(step, new Vector2(0.1f, 0.1f), 0, enemyLayer);
            Debug.DrawRay(transform.position, currentMoveDir * maxNavRayDashDistance *2f);
            if(enemies != null)
            {
                step = (Vector2)transform.position + currentMoveDir * (dashSpeed * 0.5f);
            }

            if (NavMesh.SamplePosition(step, out NavMeshHit hit, maxNavRayDashDistance, NavMesh.AllAreas))
            {
                col.enabled = false;
                canMove = false;


                transform.position = (Vector2)hit.position;
                

                canMove = true;
                col.enabled = true;
                StartCoroutine(DashCooldown());

            }
            */

            if (dashRoutine != null)
                StopCoroutine(dashRoutine);

            dashRoutine = StartCoroutine(
                MovementModule.DashByTimer(gameObject, InputReader.LastMoveValue, dashTimer, dashSpeed, 
                maxNavRayDashDistance, enemyLayer, FinishedDash));

            isDashing = true;
        }
    }

    private void FinishedDash()
    {
        isDashing = false;
        StartCoroutine(DashCooldown());
    }

    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashTimer);
        canDash = true;
    }

    #endregion

    /// <summary>
    /// Flip visuals based on a target x-axis direction
    /// </summary>
    public void FlipVisuals(float targetDir)
    {
        Vector3 localScale = visualT.localScale;
        bool targetRightFlip = targetDir > 0;
        bool localRightFlip = Mathf.Sign(localScale.x) == -1;

        // Is already flipped?
        if (targetRightFlip == localRightFlip)
            return; // No need to flip again

        visualT.localScale = new Vector3(localScale.x * -1, localScale.y, localScale.z);
    }
}
