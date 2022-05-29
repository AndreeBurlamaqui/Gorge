using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "More Damage", menuName = "Passives/More Damage")]
public class MoreDamage : PassiveAbilitySO
{
    public override void ApplyEffect()
    {

        Player.ShootingHandler.extraDamage += (int)boostVariable;

    }

    public override void ResetEffect()
    {
        Player.ShootingHandler.extraDamage = 0;
    }
}
