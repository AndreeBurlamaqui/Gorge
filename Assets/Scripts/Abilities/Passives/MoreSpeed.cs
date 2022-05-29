using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "More Speed", menuName = "Passives/More Speed")]
public class MoreSpeed : PassiveAbilitySO
{

    public override void ApplyEffect()
    {
        // multilpied by 0.01 to help on friendly numbers on scipable object
        Player.MovementHandler.extraSpeed += (boostVariable * 0.005f); 
    }

    public override void ResetEffect()
    {
        Player.MovementHandler.extraSpeed = 0;
    }
}
