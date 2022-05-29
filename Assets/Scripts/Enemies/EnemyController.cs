using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    

    public void Death()
    {
    
        // TODO: Despawn on Pool
        Destroy(gameObject);
    }

}
