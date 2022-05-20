using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveAbilitySO : ScriptableObject
{

    public float cooldownTime, bulletSpeed, destroyTime, ammo;
    public GameObject bulletPrefab;


    /// <summary>
    /// Shoot based on the current active ability
    /// </summary>
    /// <param name="barrelPosition"> Where the bullet will be instantiated</param>
    /// <param name="direction">Direction of bullet in quaternion. Simple use is to just send the Transform.Rotation of the barrel</param>
    public virtual void Shoot(Transform barrelPosition, Quaternion direction) { }

    public virtual void OnDestroyEvent(Vector2 currentPosition) { }
    
}
