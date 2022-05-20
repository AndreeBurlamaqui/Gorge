using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "InputReader", menuName = "Input Readers")]
public class InputReader : ScriptableObject, InputMap.IGameplayActions
{
    #region STATIC FIELDS

    //Device related

    public static InputType CurrentDeviceType;

    public static bool IsCurrentDeviceGamepad
    {
        get => CurrentDeviceType == InputType.Gamepad;
    }

    public static bool IsCurrentDeviceDualshock()
    {
        if (CurrentInputDevice == null)
            return false;

        return CurrentInputDevice.ToString().IndexOf("Dualshock", StringComparison.OrdinalIgnoreCase) != -1;
    }

    public static InputDevice CurrentInputDevice;

    private static Camera mainCamera;
    public static Camera MainCam
    {
        get
        {
            if (mainCamera == null)
                mainCamera = Camera.main;

            return mainCamera;
        }
    }

    public static Vector2 LastMoveValue;
    public static Vector2 LastAimValue;

    public static float BaseRadiusLimit = 6;

    #endregion

    #region INPUT ACTION EVENTS
    //Action events

    // Movement
    public event UnityAction<Vector3, bool> OnMoveEvent;
    public event UnityAction<bool> OnDashEvent;
    public event UnityAction<bool> OnShootEvent;

    #endregion

    #region MOVEMENT RELATED INPUTS EVENTS RECEIVER

    public void OnMovement(InputAction.CallbackContext context)
    {
        if (!gameplayActionState)
            return;

        if (context.control.noisy || context.control.device.noisy)
            return;

        Vector2 dir = context.ReadValue<Vector2>();
        bool isPerforming = dir != Vector2.zero;

        if (isPerforming)
        {
            LastMoveValue = dir;
        }

        CurrentDeviceType = GetInputType(context.control);

        OnMoveEvent?.Invoke(dir, isPerforming);
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        OnShootEvent?.Invoke(context.performed);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        OnDashEvent?.Invoke(context.performed);
    }

    public void OnAim(InputAction.CallbackContext context)
    {

    }

    #endregion


    #region Input Helpers

    /// <summary>
    /// Get a simple input type. I.e. Gamepad or Others.
    /// <para>Supports InputControl and InputDevice types</para>
    /// </summary>
    public static InputType GetInputType(InputControl input)
    {
        return input.device.ToString()
.IndexOf("Gamepad", StringComparison.OrdinalIgnoreCase) != -1 ? InputType.Gamepad : InputType.Keyboard;
    }

    /// <summary>
    /// Update the CurrentDevice and CurrentInputDevice.
    /// <para>Supports InputControl and InputDevice type</para>
    /// </summary>
    public static void UpdateCurrentDevice(InputControl input)
    {

        Debug.Log("Update Device request");

        // Update currentDevice
        // Update CurrentInputDevice

        InputType newType = GetInputType(input);

        if (CurrentDeviceType == newType)
            return; // new value is equal the old value, no need to change anything

        CurrentDeviceType = newType;

        CurrentInputDevice = input.device;

        //I_CanvasManager.Instance.OnUpdateDevice?.Invoke(); // Change all UI input related

        Debug.Log("Device is now " + CurrentDeviceType.ToString());

    }

    /// <summary>
    /// If the <paramref name="asDevice"/> is the same as the <see cref="CurrentInputDevice"/>
    /// <para>Supports InputControl and InputDevice type</para>
    /// </summary>
    public static bool IsCurrentDeviceEqualsAs(InputControl asDevice)
    {
        return CurrentInputDevice != null && GetInputType(asDevice) == CurrentDeviceType;
    }


    public static Vector2? MousePosition
    {
        get
        {
            if (EventSystem.current != null)
            {

                if (Mouse.current != null)
                {
                    Vector3 mousePos = Mouse.current.position.ReadValue();
                    mousePos.z = MainCam.farClipPlane * 0.5f;
                    return mousePos;
                }
            }


            return null;
        }
    }

    /// <summary>
    /// Get the Mouse position based on a raycast hit on a target layermask
    /// <para>It uses the base position in case it needs to be the gamepad.</para>
    /// </summary>
    public static Vector3? World3DMousePosition(LayerMask targetLayer, Vector3 basePosition)
    {
        Ray r = MainCam.ScreenPointToRay(GetInputCoordinate(basePosition));

        if (Physics.Raycast(r, out RaycastHit screenHit, Mathf.Infinity, targetLayer))
        {
            return screenHit.point;
        }

        return null;
    }

    /// <summary>
    /// Get the screen position (coordinate) of the current "cursor" input.
    /// <para>It can be the mouse or the aim stick or the movement stick</para>
    /// </summary>
    public static Vector3 GetInputCoordinate(Vector3 relatedPosition)
    {
        return MainCam.WorldToScreenPoint((Vector3)LastMoveValue * BaseRadiusLimit + relatedPosition);
    }

    /// <summary>
    /// Get the direction of the current aim input.
    /// <para>It can be the mouse or the aim stick</para>
    /// </summary>
    /// <param name="basePosition">The related position in case we need</param>
    public static Vector3 GetAimDirection(Vector3 basePosition)
    {
        if (CurrentDeviceType == InputType.Gamepad)
        {
            return LastAimValue;
        }
        else
        {
            return MousePosition.Value - (Vector2)MainCam.WorldToScreenPoint(basePosition);
        }
    }

    /// <summary>
    /// Get the ray related to the input.
    /// <para>If it's a mouse, then it'll be a world point. But if it's a gamepad, it'll be a direction ray</para>
    /// </summary>
    public static Ray GetInputRay(Vector3 relatedPos)
    {
        Vector3 dir = LastMoveValue;
        Vector3 fixedDir = new Vector3(dir.x, 0, dir.y);

        // Offset in case the root is on the ground
        Vector3 fixedPos = new Vector3(relatedPos.x, relatedPos.y + 0.2f, relatedPos.z);
        return new Ray(fixedPos, fixedDir);
    }

    #endregion


    #region Initiate PlayerActions

    bool initiated = false;
    //Infinite_Inputs.Infinite_GameplayActions gameplayActions;

    //Call OnEnable in playerScript
    public void EnableActions()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.GorgeInputs.Gameplay.SetCallbacks(this);


        // Reset movement related variables
        CurrentDeviceType = InputType.None;
        LastMoveValue = Vector2.zero;
        LastAimValue = Vector2.zero;

        initiated = true;

        // Call to update all UI
        //I_CanvasManager.Instance.OnUpdateDevice?.Invoke();

        GameplayState = true;
    }

    //Call OnDisable in playerScript
    public void DisableActions()
    {

        GameplayState = false;

        initiated = false;

    }

    public static UnityAction<bool> OnGameplayStateEvent;

    private bool gameplayActionState;
    public bool GameplayState
    {
        get => gameplayActionState;
        set
        {
            if (!initiated)
                return;

            if (initiated && gameplayActionState == value)
                return; // Same value. No need to update

            if (GameManager.Instance != null)
            {
                if (value)
                {
                    GameManager.Instance.GorgeInputs.Gameplay.Enable();
                }
                else
                {
                    GameManager.Instance.GorgeInputs.Gameplay.Disable();
                }
            }

            gameplayActionState = value;
            OnGameplayStateEvent?.Invoke(value);
        }
    }


    #endregion Initiate PlayerActions
}

public enum InputType
{
    None,
    Keyboard,
    Gamepad
}
