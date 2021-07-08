using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="new drop", menuName ="Drops")]
public class Drop : ScriptableObject
{
    public Color cor;
    public new string name;
    public string effect;
    public Sprite image;

    public ActiveAbilitySO active;
    public PassiveAbilitySO passive;
    
}
