// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Player_Slime/InputMap.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @InputMap : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @InputMap()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputMap"",
    ""maps"": [
        {
            ""name"": ""Action"",
            ""id"": ""c72533d9-139d-4d52-9f04-2ac4c24e2214"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2fc88bef-6ac4-4a84-abef-16278634c21d"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MousePos"",
                    ""type"": ""Value"",
                    ""id"": ""7b47e3d1-36ac-4752-9c3e-e1e9a4e71996"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Shoot"",
                    ""type"": ""Button"",
                    ""id"": ""0fe19151-b103-433b-ba32-9dd60a34658d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""PassThrough"",
                    ""id"": ""86ec3a42-1946-4f20-8961-483920211ba9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2a023065-8095-473b-89ed-901038e6934c"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Slime"",
                    ""action"": ""MousePos"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09524130-f048-48ed-8e61-4c276d468a9e"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Slime"",
                    ""action"": ""Shoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Keyboard"",
                    ""id"": ""e064a3c3-6962-45da-be8c-9b1dc0c2df49"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""302226ae-cbcb-46af-b118-1dc3fa739ce8"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Slime"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""4a875c2d-74e2-41e2-a0d3-efdb1aeb472b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Slime"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""4654b1c8-4446-4810-b28d-7aa75a8cf029"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Slime"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""f9cceedd-f062-4116-b094-641f4432a691"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Slime"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""b97d4297-0802-4eab-a26d-3e847871d310"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Slime"",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Slime"",
            ""bindingGroup"": ""Slime"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": true,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": true,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Action
        m_Action = asset.FindActionMap("Action", throwIfNotFound: true);
        m_Action_Movement = m_Action.FindAction("Movement", throwIfNotFound: true);
        m_Action_MousePos = m_Action.FindAction("MousePos", throwIfNotFound: true);
        m_Action_Shoot = m_Action.FindAction("Shoot", throwIfNotFound: true);
        m_Action_Dash = m_Action.FindAction("Dash", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }

    // Action
    private readonly InputActionMap m_Action;
    private IActionActions m_ActionActionsCallbackInterface;
    private readonly InputAction m_Action_Movement;
    private readonly InputAction m_Action_MousePos;
    private readonly InputAction m_Action_Shoot;
    private readonly InputAction m_Action_Dash;
    public struct ActionActions
    {
        private @InputMap m_Wrapper;
        public ActionActions(@InputMap wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Action_Movement;
        public InputAction @MousePos => m_Wrapper.m_Action_MousePos;
        public InputAction @Shoot => m_Wrapper.m_Action_Shoot;
        public InputAction @Dash => m_Wrapper.m_Action_Dash;
        public InputActionMap Get() { return m_Wrapper.m_Action; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionActions set) { return set.Get(); }
        public void SetCallbacks(IActionActions instance)
        {
            if (m_Wrapper.m_ActionActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnMovement;
                @MousePos.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnMousePos;
                @MousePos.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnMousePos;
                @MousePos.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnMousePos;
                @Shoot.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnShoot;
                @Shoot.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnShoot;
                @Shoot.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnShoot;
                @Dash.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnDash;
            }
            m_Wrapper.m_ActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @MousePos.started += instance.OnMousePos;
                @MousePos.performed += instance.OnMousePos;
                @MousePos.canceled += instance.OnMousePos;
                @Shoot.started += instance.OnShoot;
                @Shoot.performed += instance.OnShoot;
                @Shoot.canceled += instance.OnShoot;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
            }
        }
    }
    public ActionActions @Action => new ActionActions(this);
    private int m_SlimeSchemeIndex = -1;
    public InputControlScheme SlimeScheme
    {
        get
        {
            if (m_SlimeSchemeIndex == -1) m_SlimeSchemeIndex = asset.FindControlSchemeIndex("Slime");
            return asset.controlSchemes[m_SlimeSchemeIndex];
        }
    }
    public interface IActionActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMousePos(InputAction.CallbackContext context);
        void OnShoot(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
    }
}
