using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Faster Bullets", menuName = "Passives/Faster Bullets")]
public class MoreFastBullets : PassiveAbilitySO
{
    public override void ApplyEffect()
    {
        FindObjectOfType<PlayerShooting>().shootTimerReductor += boostVariable;
    }
}
