using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "More Damage", menuName = "Passives/More Damage")]
public class MoreDamage : PassiveAbilitySO
{
    public override void ApplyEffect()
    {

        FindObjectOfType<PlayerShooting>().extraDamage += (int)boostVariable;

    }
}
