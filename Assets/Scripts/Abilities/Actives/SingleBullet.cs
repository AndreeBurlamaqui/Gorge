using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Single Bullet", menuName = "Actives/Single Bullet")]
public class SingleBullet : ActiveAbilitySO
{

    public override void Shoot(Transform barrelPosition, Quaternion direction)
    {
        GameObject bulletGO = Instantiate(bulletPrefab, barrelPosition.position, direction);

        if (bulletGO.TryGetComponent(out GeneralBullet gb))
        {
            gb.SetBulletVariables(bulletSpeed, destroyTime, this);
        }

    }
}
