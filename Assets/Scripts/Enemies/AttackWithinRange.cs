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
    public float offsetPosAim = 5;
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
            UpdateAim();

            if (canShoot)
            {
                shootingType.Shoot(triggerPosition, rootTrigger.rotation);
                StartCoroutine(ShootCooldown(shootingType.cooldownTime));

                muzzleFX.SetActive(true);

                shootAnim.Play();
            }
        }
    }

    private void UpdateAim()
    {
        Vector2 dir = player.position - rootTrigger.position;

        // Position
        //rootTrigger.position = transform.position + (Vector3)(offsetPosAim * dir.normalized);

        // Rotation
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //rootTrigger.rotation = Quaternion.AngleAxis(mAngle - 90f, Vector3.forward);
        rootTrigger.eulerAngles = new Vector3(0, 0, angle);

    }

    IEnumerator ShootCooldown(float canShootTimer)
    {
        canShoot = false;
        yield return new WaitForSeconds(canShootTimer);
        canShoot = true;
    }

}
