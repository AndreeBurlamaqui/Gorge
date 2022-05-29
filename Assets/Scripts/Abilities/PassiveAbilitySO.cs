using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PassiveAbilitySO : ScriptableObject
{
    PlayerController _controller;
    public PlayerController Player
    {
        get
        {
            if (_controller == null)
                _controller = FindObjectOfType<PlayerController>();

            return _controller;
        }
    }

    public float boostVariable;

    /// <summary>
    /// Aplicar efeito da passiva.
    /// </summary>
    /// <param name="targetGO">Game Object alvo para aplicar a passiva, caso for inimigo será útil.</param>
    public abstract void ApplyEffect();
    public abstract void ResetEffect();
}
