using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class AttackWithinRange : MonoBehaviour
{
    public ActiveAbilitySO shootingType;
    public bool onSight, canShoot = true;
    public Transform rootTrigger, triggerPosition, player;
    public GameObject muzzleFX;
    public AnimationStateReference shootAnim;
    private void Start()
    {
        GetComponent<ChaserEnemy>().OnSightEvent.AddListener(delegate { onSight = true; });
        GetComponent<ChaserEnemy>().LostSightEvent.AddListener(delegate { onSight = false; });

        player = FindObjectOfType<Player_Movement>().gameObject.transform;
    }

    private void Update()
    {
        if(onSight)
        {
            Vector2 dir = player.position - transform.position;
            float mAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rootTrigger.rotation = Quaternion.AngleAxis(mAngle - 90f, Vector3.forward);

            if (canShoot)
            {
                shootingType.Shoot(triggerPosition, mAngle);
                StartCoroutine(ShootCooldown(shootingType.cooldownTime));

                muzzleFX.SetActive(true);

                shootAnim.Play();
            }
        }
    }

    IEnumerator ShootCooldown(float canShootTimer)
    {
        canShoot = false;
        yield return new WaitForSeconds(canShootTimer);
        canShoot = true;
    }

}
