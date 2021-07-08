using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    public GameObject eye;

    [Header("Walk")]
    public bool canMove = true;
    public float baseSpeed, maxNavRayStepDistance;

    [Header("Dash")]
    public bool canDash = true;
    public float maxNavRayDashDistance, dashSpeed, dashTimer;
    public LayerMask enemyLayer;

    private InputMap _inputMap;
    private Vector2 currentMoveDir;
    private float clampSpeed, movSpeed;
    private Collider2D col;
    private Animator anim;


    private void Awake()
    {
        _inputMap = new InputMap();
        _inputMap.Action.Dash.performed += ctx => OnDash();

        anim = GetComponentInChildren<Animator>();
        col = GetComponent<Collider2D>();
    }

    private void OnEnable()
    {
        _inputMap.Enable();
    }
    private void OnDisable()
    {
        _inputMap.Disable();
    }

    public Vector2 GetMoveDirection()
    {
        Vector2 moveDir = _inputMap.Action.Movement.ReadValue<Vector2>();
        clampSpeed = Mathf.Clamp(moveDir.magnitude, 0f, 1f);
        moveDir.Normalize();

        return moveDir;
    }

    // Update is called once per frame
    void Update()
    {
        if (_inputMap.Action.Movement.ReadValue<Vector2>().magnitude > 0.1f)
        {
            currentMoveDir = GetMoveDirection();
            if (canMove)
            {
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

            if(currentMoveDir.x != 0)
                anim.SetFloat("Blend", currentMoveDir.x);


            if (currentMoveDir.y >= 0.1f)
            {
                eye.SetActive(false);
                //up
            }
            else
            {
                eye.SetActive(true);
                //down
            }

        }
        else
        {
            if (anim.GetBool("isWalking"))
                anim.SetBool("isWalking", false);
        }


    }

    public void Moving()
    {
        canMove = !canMove;
    }

    private void OnDash()
    {
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
}
