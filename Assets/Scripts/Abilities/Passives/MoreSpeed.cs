using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "More Speed", menuName = "Passives/More Speed")]
public class MoreSpeed : PassiveAbilitySO
{
    public override void ApplyEffect()
    {
        FindObjectOfType<Player_Movement>().CurrentSpeed += (boostVariable * 0.1f);
    }
}
