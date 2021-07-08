using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Actives/Shotgun")]
public class ShotgunBullet : ActiveAbilitySO
{
    public int pelletCount;
    public float cooldownBetweenPellet;
    public Vector2 angleRange;
    public override void Shoot(Transform barrelPosition, float direction)
    {

            for (int x = 0; x < pelletCount; x++)
            {
                GameObject bulletGO = Instantiate(bulletPrefab, barrelPosition.position, Quaternion.identity);
                bulletGO.transform.rotation = Quaternion.AngleAxis((direction - 90f) + Random.Range(angleRange.x, angleRange.y), Vector3.forward);

                if (bulletGO.TryGetComponent(out GeneralBullet gb))
                {
                    gb.SetBulletVariables(bulletSpeed, destroyTime, this);
                }

            }
        
        

    }


    
}
