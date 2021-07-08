using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Shoot Then Drop", menuName = "Actives/Shoot Then Drop")]
public class ShootThenDrop : ActiveAbilitySO
{
    public GameObject dropPrefab;
    public Drop toDrop;
    public override void Shoot(Transform barrelPosition, float direction)
    {
        if (ammo == 1)
            FindObjectOfType<Stomach_Inventory>().RemoveActive();

        if (ammo > 0)
        {

            GameObject bulletGO = Instantiate(bulletPrefab, barrelPosition.position, Quaternion.identity);
            bulletGO.transform.rotation = Quaternion.AngleAxis(direction - 90f, Vector3.forward);

            if (bulletGO.TryGetComponent(out GeneralBullet gb))
            {
                gb.SetBulletVariables(bulletSpeed, destroyTime, this);
            }

            ammo--;
        }

    }

    public override void OnDestroyEvent(Vector2 currentPosition)
    {
        GameObject dPF = Instantiate(dropPrefab, currentPosition, Quaternion.identity);
        dPF.GetComponent<Pickup>().dropSO = Instantiate(toDrop);
    }
}
