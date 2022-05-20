using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Actives/Shotgun")]
public class ShotgunBullet : ActiveAbilitySO
{
    public int pelletCount;
    public float cooldownBetweenPellet;
    public Vector2 angleRange;
    public override void Shoot(Transform barrelPosition, Quaternion direction)
    {

            for (int x = 0; x < pelletCount; x++)
            {
                GameObject bulletGO = Instantiate(bulletPrefab, barrelPosition.position, direction);

                if (bulletGO.TryGetComponent(out GeneralBullet gb))
                {
                    gb.SetBulletVariables(bulletSpeed, destroyTime, this);
                }

            }
        
        

    }


    
}
