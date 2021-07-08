using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavMeshClearColliders : MonoBehaviour
{
    private void OnEnable()
    {
        Collider2D[] childCols = GetComponentsInChildren<Collider2D>();

        foreach(Collider2D cc in childCols)
        {
            cc.enabled = false;
        }
    }
}
