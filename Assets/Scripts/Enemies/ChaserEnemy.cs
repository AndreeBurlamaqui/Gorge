using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class ChaserEnemy : MonoBehaviour
{
    public GameObject eye;
    private Transform player;
    private NavMeshAgent agent;
    private HealthManager hm;
    //patrol
    public float walkRadius, waitToPatrolAgainTimer;
    //public Transform[] moveSpots;
    //private int randomSpots;
    private Vector2 newDestination;
    private bool goingToDestination = false;
    public bool isPatrol = false, stayFar = false, stayHide = false;
    //chasing
    public float sightRange = 10f;
    public float stayFarMultiplier = 1;
    [SerializeField] private bool canWalk = true;

    public UnityEvent OnSightEvent;
    public UnityEvent LostSightEvent;

    private bool alreadyOnSight;

    private Animator anim;
    void Start()
    {
        //randomSpots = Random.Range(0, moveSpots.Length);

        agent = GetComponent<NavMeshAgent>();
        player = FindObjectOfType<Player_Movement>().transform;

        agent.updateRotation = false;
        agent.updateUpAxis = false;

        hm = GetComponent<HealthManager>();
        hm.OnHitEvent.AddListener(delegate { StartCoroutine(WasHit()); });

        anim = GetComponentInChildren<Animator>();
        

        if (isPatrol)
            UpdateWayPoints();
    }

    void ChasePlayer()
    {

        Vector3 targetPos;

        if (stayFar)
        {
            targetPos = player.position + -((player.position - transform.position).normalized * (agent.stoppingDistance * stayFarMultiplier));
        }
        else
        {
            targetPos = player.position;
        }


        //Tirei o chase do navmesh pra custar menos
        //agent.SetDestination(targetPos);
        transform.position = Vector2.MoveTowards(transform.position, targetPos, (agent.speed / 2) * Time.deltaTime);

        if (agent.enabled)
            agent.enabled = false;
    }
    void Patrol()
    {

        if (!goingToDestination)
        {
            UpdateWayPoints();

            NavMeshPath newPath = new NavMeshPath();

            if (agent.CalculatePath(newDestination, newPath) && newPath.status == NavMeshPathStatus.PathComplete)
            {
                agent.SetDestination(newDestination);

                goingToDestination = true;
            }
            else
            {
                UpdateWayPoints();
            }

        }

        if (Vector2.Distance(transform.position, newDestination) < 0.02f)
        {
            StartCoroutine(WaitTime());
        }
    }

    void UpdateWayPoints()
    {
        if (agent.enabled)
        {
            Vector2 randomDirection = (Vector2)transform.position + Random.insideUnitCircle * walkRadius;

            NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, walkRadius, NavMesh.AllAreas);

            NavMeshPath newPath = new NavMeshPath();
            agent.CalculatePath(hit.position, newPath);
            NavMeshPathStatus pathStatus = newPath.status;
            if (pathStatus == NavMeshPathStatus.PathComplete)
            {
                newDestination = hit.position;

                goingToDestination = false;
            }
            else
            {
                UpdateWayPoints();
            }
        }
        
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (player != null)
        {
            

            if (canWalk && Vector2.Distance(transform.position, player.position) < (stayHide? sightRange / 2f : sightRange))
            {
                if (goingToDestination)
                    goingToDestination = false;

                if (!alreadyOnSight)
                {
                    alreadyOnSight = true;
                    OnSightEvent.Invoke();
                }

                if (!stayHide)
                {
                    
                    ChasePlayer();
                }

            }
            else
            {
                if (!agent.enabled)
                    agent.enabled = true;

                if (!goingToDestination)
                {
                    Patrol();
                }

                if (alreadyOnSight)
                {

                    alreadyOnSight = false;
                    LostSightEvent.Invoke();
                }
            }


                
            
            



            if (agent.velocity.x > 0.1)
            {
                //right
                anim.SetBool("isRight", true);
            }
            else if (agent.velocity.x < -0.1)
            {
                //left
                anim.SetBool("isRight", false);
            }

            if (agent.velocity.y > 0.1)
            {
                //up
                eye.SetActive(false);
            }
            else if (agent.velocity.y < -0.1)
            {
                //down
                eye.SetActive(true);
            }
        }


    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(waitToPatrolAgainTimer);
        UpdateWayPoints();
    }
    private IEnumerator WasHit()
    {
        canWalk = false;
        yield return new WaitForSeconds(hm.knockbackDuration);
        canWalk = true;
    }

    

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, walkRadius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(newDestination, 0.5f);


        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }
}
