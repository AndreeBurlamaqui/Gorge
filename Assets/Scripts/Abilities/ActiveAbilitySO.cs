using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbilitySO : ScriptableObject
{

    public float cooldownTime, bulletSpeed, destroyTime, ammo;
    public GameObject bulletPrefab;


    /// <summary>
    /// Vari�vel que precisa ser chamada para atirar. Todo ScriptableObject vai ter um tiro �nico.
    /// </summary>
    /// <param name="barrelPosition"> Aonde a bala ir� ser instanciada</param>
    /// <param name="direction">A dire�ao da bala, no script PlayerShooting � o mAngle</param>
    public virtual void Shoot(Transform barrelPosition, float direction) { }

    public virtual void OnDestroyEvent(Vector2 currentPosition) { }
    
}
