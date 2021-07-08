using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Single Bullet", menuName = "Actives/Single Bullet")]
public class SingleBullet : ActiveAbilitySO
{

    public override void Shoot(Transform barrelPosition, float direction)
    {
        GameObject bulletGO = Instantiate(bulletPrefab, barrelPosition.position, Quaternion.identity);
        bulletGO.transform.rotation = Quaternion.AngleAxis(direction - 90f, Vector3.forward);

        if (bulletGO.TryGetComponent(out GeneralBullet gb))
        {
            gb.SetBulletVariables(bulletSpeed, destroyTime, this);
        }

    }
}
