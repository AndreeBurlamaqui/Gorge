using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using MyBox;
using UnityEngine.AI;
using UnityEngine.UI;
[RequireComponent(typeof(Collider2D))]
public class HealthManager : MonoBehaviour
{

    [Tag] public string beHurtBy;
    [ReadOnly][SerializeField] private Animator anim;
    [SerializeField] private AnimationStateReference OnHurtAnimation;
    [SerializeField] private AnimationStateReference OnDeathAnimation;
    public int maxLife;
    [SerializeField][ReadOnly] public int currentLife;
    [SerializeField] private GameObject deathFX, hitFX;
    public float knockbackForce, knockbackDuration;

    private Collider2D col;

    [SerializeField] private float restingTimerMax = 0.3f;
    [SerializeField] private bool isResting = false;
    public bool isDead = false;

    /// <summary>
    /// Event that will send the <see cref="maxLife"/> and <see cref="currentLife"/> when entity is hit
    /// </summary>
    [SerializeField] public UnityEvent<int,int> OnHitEvent;
    [SerializeField] public UnityEvent OnDeathEvent;
    private PlayerShooting playerShoot;

    private void Start()
    {
        currentLife = maxLife;
        anim = GetComponent<Animator>();
        col.isTrigger = true;

        
    }

    private void OnEnable()
    {
        if(col == null)
            col = GetComponent<Collider2D>();
    
        col.enabled = true;
    }

    public void ApplyHurt(int howHurt = 0)
    {
        if (howHurt > 0)
        {
            for (int x = 0; x < howHurt; x++)
            {
                currentLife--;

                if (OnHurtAnimation.Assigned)
                {
                    OnHurtAnimation.Play();
                }
            }
        }
        else
        {
            currentLife--;

            if (OnHurtAnimation.Assigned)
            {
                OnHurtAnimation.Play();
            }
        }

        if (hitFX != null)
            hitFX.SetActive(true);

        OnHitEvent.Invoke(maxLife, currentLife);

        
        CheckDeath();
    }

    private IEnumerator Death(float timeToDestroy)
    {
        //if(deathSparkleFX != null)
        //    Instantiate(deathSparkleFX, transform.position, Quaternion.identity, transform);

        yield return new WaitForSeconds(timeToDestroy);

        OnDeathEvent.Invoke();        
    }

    private void CheckDeath()
    {
        if(currentLife <= 0)
        {

            if (OnDeathAnimation.Assigned)
            {
                OnDeathAnimation.Play();
                StartCoroutine(Death(OnDeathAnimation.Animator.GetCurrentAnimatorClipInfo(0).Length));

            }
            else
            {
                StartCoroutine(Death(0));

            }

            isDead = true;

            col.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (!isDead)
        {
            if (collision.CompareTag(beHurtBy))
            {
                if (!isResting)
                {
                    Vector2 contactKnockback = collision.ClosestPoint(col.bounds.center);
                    Vector2 step = (Vector2)transform.position + -((contactKnockback - (Vector2)transform.position).normalized * knockbackForce);

                    if (NavMesh.SamplePosition(step, out NavMeshHit hit, 1.5f, NavMesh.AllAreas))
                    {

                        StartCoroutine(ApplyKnockback(hit.position));

                    }

                    StartCoroutine(Rest());
                }
               


                if (collision.CompareTag("Player Bullet"))
                {
                    if (playerShoot == null)
                        playerShoot = FindObjectOfType<PlayerShooting>();

                    ApplyHurt(playerShoot.extraDamage);

                }
                else
                {
                    ApplyHurt();
                }

                if (collision.TryGetComponent(out GeneralBullet gb))
                {
                    gb.DestroyBullet();
                }
            }

            


        }
    }

    IEnumerator ApplyKnockback(Vector2 hitPosition)
    {
        float journey = 0f;
        while (journey <= knockbackDuration)
        {
            journey += Time.deltaTime;
            float percent = Mathf.Clamp01(journey / knockbackDuration);

            //float curvePercent = shootKickCurve.Evaluate(percent);
            transform.position = Vector3.Lerp(transform.position, hitPosition, percent);

            yield return null;
        }
    }

    IEnumerator Rest()
    {
        isResting = true;
        yield return new WaitForSeconds(restingTimerMax);
        isResting = false;
    }
}
