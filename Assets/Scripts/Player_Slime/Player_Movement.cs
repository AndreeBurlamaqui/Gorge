using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    PlayerController controller;
    public GameObject eye;

    [Header("Walk")]
    public bool canMove = true;
    public float baseSpeed, maxNavRayStepDistance;
    bool isMoving = false;

    [Header("Dash")]
    public bool canDash = true;
    public float maxNavRayDashDistance, dashSpeed, dashTimer;
    public LayerMask enemyLayer;

    private InputMap _inputMap;
    private Vector2 currentMoveDir;
    private float clampSpeed, movSpeed;
    private Collider2D col;
    private Animator anim;


    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
        controller = GetComponent<PlayerController>();

        controller.actionReader.OnMoveEvent += TryMoveEvent;
        controller.actionReader.OnDashEvent += TryDashEvent;
    }

    public void TryMoveEvent(Vector3 direction, bool isPerforming)
    {
        clampSpeed = Mathf.Clamp(direction.magnitude, 0f, 1f);
        direction.Normalize();
        currentMoveDir = direction;
    }

    // Update is called once per frame
    void Update()
    {

        ApplyMove();

        if (currentMoveDir.magnitude > 0.1f)
        {
            if(currentMoveDir.x != 0)
                anim.SetFloat("Blend", currentMoveDir.x);

            if (currentMoveDir.y >= 0.1f)
            {
                // Looking up
                eye.SetActive(false);
            }
            else
            {
                // Looking down
                eye.SetActive(true);
            }

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
                    //rb.velocity = moveDir * clampSpeed * baseSpeed;
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


    private void TryDashEvent(bool isPerforming)
    {
        if (!isPerforming)
            return;

        if (canDash)
        {
            Vector2 step = (Vector2)transform.position + currentMoveDir * dashSpeed;

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

        }
    }

    IEnumerator DashCooldown()
    {
        canDash = false;
        yield return new WaitForSeconds(dashTimer);
        canDash = true;
        
    }

    public float CurrentSpeed
    {
        get
        {
            if(movSpeed <= 0)
            {
                movSpeed = baseSpeed;
            }

            return movSpeed;
        }
        set => movSpeed = baseSpeed + value;
    }

    public bool IsValidSituation()
    {
        if (!canMove)
            return false;

        return true;
    }
}
