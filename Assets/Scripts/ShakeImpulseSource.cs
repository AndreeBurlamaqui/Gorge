using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ShakeImpulseSource : MonoBehaviour
{
    private CinemachineImpulseSource cis;
    private void OnEnable()
    {
        if (cis == null)
            cis = GetComponent<CinemachineImpulseSource>();

        cis.GenerateImpulse();
    }
}
