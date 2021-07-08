using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropEnemy : MonoBehaviour
{
    public GameObject objDrop;
    public Drop drop;
    private void Start()
    {
        GetComponent<HealthManager>().OnDeathEvent.AddListener(Death);
    }
    public void Death()
    {
        //trigger for enemy death then spawn the item
        GameObject TempPrefab = Instantiate(objDrop, transform.position, Quaternion.identity);
        TempPrefab.name = "Drop " + drop.name;
        TempPrefab.GetComponent<Pickup>().dropSO = Instantiate(drop);

    }

}
