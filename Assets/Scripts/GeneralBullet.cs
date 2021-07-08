using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]
public class GeneralBullet : MonoBehaviour
{
    private Rigidbody2D rb;
    private float speed, destroyTimer;
    private ActiveAbilitySO shootType;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        rb.velocity = transform.up * speed * Time.deltaTime * 2;

        destroyTimer -= Time.deltaTime;

        if (destroyTimer <= 0)
        {
            DestroyBullet();
        }
    }

    public void SetBulletVariables(float newSpeed, float newDestroyTime, ActiveAbilitySO newShootType)
    {
        speed = newSpeed;
        destroyTimer = newDestroyTime;
        shootType = newShootType;
    }

    public void DestroyBullet()
    {
        if (NavMesh.SamplePosition(transform.position, out NavMeshHit hit, 20f, NavMesh.AllAreas))
        {
            shootType.OnDestroyEvent(hit.position);
        }

        Destroy(gameObject);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<NavMeshModifier>()){
            DestroyBullet();
        }
    }
}
