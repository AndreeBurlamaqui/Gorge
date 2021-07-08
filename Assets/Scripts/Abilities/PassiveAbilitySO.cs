using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveAbilitySO : ScriptableObject
{
    public float boostVariable;

    /// <summary>
    /// Aplicar efeito da passiva.
    /// </summary>
    /// <param name="targetGO">Game Object alvo para aplicar a passiva, caso for inimigo será útil.</param>
    public virtual void ApplyEffect() { }
}
