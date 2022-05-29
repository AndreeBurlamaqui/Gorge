using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shotgun", menuName = "Actives/Shotgun")]
public class ShotgunBullet : ActiveAbilitySO
{
    public int pelletCount;
    public float cooldownBetweenPellet;
    public bool isRandomPellets = false;

    [Header("Min - Max range")]
    public Vector2 angleRange;

    public override void Shoot(Transform barrelPosition, Quaternion direction)
    {

        for (int x = 1; x <= pelletCount; x++)
        {
            float randomAngle = isRandomPellets ?
                Random.Range(angleRange.x, angleRange.y) :
                Mathf.Lerp(angleRange.x, angleRange.y, (float)x / pelletCount);

            Quaternion spreadRot = Quaternion.Euler(direction.eulerAngles.x, direction.eulerAngles.y, 
                direction.eulerAngles.z + randomAngle);

            GameObject bulletGO = Instantiate(bulletPrefab, barrelPosition.position, spreadRot);

            if (bulletGO.TryGetComponent(out GeneralBullet gb))
            {
                gb.SetBulletVariables(bulletSpeed, destroyTime, this);
            }

        }



    }


    
}
