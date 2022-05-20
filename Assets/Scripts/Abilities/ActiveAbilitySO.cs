using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbilitySO : ScriptableObject
{

    public float cooldownTime, bulletSpeed, destroyTime, ammo;
    public GameObject bulletPrefab;


    /// <summary>
    /// Variável que precisa ser chamada para atirar. Todo ScriptableObject vai ter um tiro único.
    /// </summary>
    /// <param name="barrelPosition"> Aonde a bala irá ser instanciada</param>
    /// <param name="direction">A direçao da bala, no script PlayerShooting é o mAngle</param>
    public virtual void Shoot(Transform barrelPosition, Quaternion direction) { }

    public virtual void OnDestroyEvent(Vector2 currentPosition) { }
    
}
