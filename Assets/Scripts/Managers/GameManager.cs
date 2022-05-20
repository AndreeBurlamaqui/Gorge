using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    #region SINGLETON

    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    #endregion


    [Header("Action Asset")]
    public InputActionAsset GorgeActions;
    public InputMap GorgeInputs { get; private set; }

    private void Awake()
    {
        GorgeInputs = new InputMap();
    }

}
