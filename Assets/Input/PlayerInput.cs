//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Input/PlayerInput.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerInput : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""9371662e-1d9a-416f-b26b-b2a0a0a346e6"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""2901f9d7-533b-4b56-b01b-0ae77355fadb"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""f15a23d8-e0dd-46f6-8348-f6f65384903a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera Rotate Clockwise"",
                    ""type"": ""Button"",
                    ""id"": ""17f1a1c4-7fa4-47cd-a6dd-41668bd14214"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Camera Rotate Counterclockwise"",
                    ""type"": ""Button"",
                    ""id"": ""b1b42575-de64-4590-9d56-1cead55bf320"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""F"",
                    ""type"": ""Button"",
                    ""id"": ""e5b58c9a-a514-41b4-b6e0-86add90a40b9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Tab"",
                    ""type"": ""Button"",
                    ""id"": ""c02bb1e1-74c5-4e65-b5a4-91d343e363c3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TeleportKey"",
                    ""type"": ""Button"",
                    ""id"": ""52c8b464-7f52-4d2d-b0b9-431495d903a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""c88be8a1-dc8b-4750-92ce-69f3cea04aed"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""58f86ec1-2bcd-422b-af08-017231cb19f8"",
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
                    ""id"": ""e8f5b859-64f4-44bc-9d86-04ad6ae0a709"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""126c77ea-ce4e-4797-8849-2abffb821dbe"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""1f1e5f62-c221-40fb-adf2-e70137266cec"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""280bf079-b1c0-4bec-9c91-ef2aefba9086"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""37ca6133-72fa-4d58-b10f-07243a8f51b0"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fd53f35f-a341-46cd-964a-737e6b06951c"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""94320ac1-ba58-4f38-b571-34641dd242d3"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Rotate Clockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""19a0e648-232f-4191-9e90-8a755dbb31f7"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Rotate Clockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""72a5245c-1601-46dd-b6ea-6e91bc550c5e"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Rotate Counterclockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8d3ed19c-fa7a-4d86-8627-77f6e9196f1a"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Camera Rotate Counterclockwise"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""feceb199-99b6-409a-a3f5-c203ea8bab30"",
                    ""path"": ""<Keyboard>/f"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""F"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""20fda9f1-f136-428d-82cf-3c05dff644c3"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Tab"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""85849508-014f-4eaa-9496-4eaa433d6d21"",
                    ""path"": ""<Keyboard>/t"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TeleportKey"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Menu"",
            ""id"": ""eadb8d52-ea61-4875-9993-b87eee3c97fa"",
            ""actions"": [
                {
                    ""name"": ""Open and Close menu"",
                    ""type"": ""Button"",
                    ""id"": ""bae12d09-1ac8-4dea-85a2-14428ea4a727"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""35817fc4-b06d-4868-b197-92bb0fd1961c"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Open and Close menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cac427e3-658c-49c9-863d-2c621ee20c0d"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Open and Close menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c0c24566-7ec4-4bb1-a171-f02f232128a1"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Open and Close menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6daf5df6-c4cc-43c8-88fc-7034b0a475fd"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Open and Close menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_Jump = m_Player.FindAction("Jump", throwIfNotFound: true);
        m_Player_CameraRotateClockwise = m_Player.FindAction("Camera Rotate Clockwise", throwIfNotFound: true);
        m_Player_CameraRotateCounterclockwise = m_Player.FindAction("Camera Rotate Counterclockwise", throwIfNotFound: true);
        m_Player_F = m_Player.FindAction("F", throwIfNotFound: true);
        m_Player_Tab = m_Player.FindAction("Tab", throwIfNotFound: true);
        m_Player_TeleportKey = m_Player.FindAction("TeleportKey", throwIfNotFound: true);
        // Menu
        m_Menu = asset.FindActionMap("Menu", throwIfNotFound: true);
        m_Menu_OpenandClosemenu = m_Menu.FindAction("Open and Close menu", throwIfNotFound: true);
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
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_Jump;
    private readonly InputAction m_Player_CameraRotateClockwise;
    private readonly InputAction m_Player_CameraRotateCounterclockwise;
    private readonly InputAction m_Player_F;
    private readonly InputAction m_Player_Tab;
    private readonly InputAction m_Player_TeleportKey;
    public struct PlayerActions
    {
        private @PlayerInput m_Wrapper;
        public PlayerActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @Jump => m_Wrapper.m_Player_Jump;
        public InputAction @CameraRotateClockwise => m_Wrapper.m_Player_CameraRotateClockwise;
        public InputAction @CameraRotateCounterclockwise => m_Wrapper.m_Player_CameraRotateCounterclockwise;
        public InputAction @F => m_Wrapper.m_Player_F;
        public InputAction @Tab => m_Wrapper.m_Player_Tab;
        public InputAction @TeleportKey => m_Wrapper.m_Player_TeleportKey;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Jump.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJump;
                @CameraRotateClockwise.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotateClockwise;
                @CameraRotateClockwise.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotateClockwise;
                @CameraRotateClockwise.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotateClockwise;
                @CameraRotateCounterclockwise.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotateCounterclockwise;
                @CameraRotateCounterclockwise.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotateCounterclockwise;
                @CameraRotateCounterclockwise.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnCameraRotateCounterclockwise;
                @F.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnF;
                @F.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnF;
                @F.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnF;
                @Tab.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTab;
                @Tab.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTab;
                @Tab.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTab;
                @TeleportKey.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTeleportKey;
                @TeleportKey.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTeleportKey;
                @TeleportKey.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnTeleportKey;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @CameraRotateClockwise.started += instance.OnCameraRotateClockwise;
                @CameraRotateClockwise.performed += instance.OnCameraRotateClockwise;
                @CameraRotateClockwise.canceled += instance.OnCameraRotateClockwise;
                @CameraRotateCounterclockwise.started += instance.OnCameraRotateCounterclockwise;
                @CameraRotateCounterclockwise.performed += instance.OnCameraRotateCounterclockwise;
                @CameraRotateCounterclockwise.canceled += instance.OnCameraRotateCounterclockwise;
                @F.started += instance.OnF;
                @F.performed += instance.OnF;
                @F.canceled += instance.OnF;
                @Tab.started += instance.OnTab;
                @Tab.performed += instance.OnTab;
                @Tab.canceled += instance.OnTab;
                @TeleportKey.started += instance.OnTeleportKey;
                @TeleportKey.performed += instance.OnTeleportKey;
                @TeleportKey.canceled += instance.OnTeleportKey;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);

    // Menu
    private readonly InputActionMap m_Menu;
    private IMenuActions m_MenuActionsCallbackInterface;
    private readonly InputAction m_Menu_OpenandClosemenu;
    public struct MenuActions
    {
        private @PlayerInput m_Wrapper;
        public MenuActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @OpenandClosemenu => m_Wrapper.m_Menu_OpenandClosemenu;
        public InputActionMap Get() { return m_Wrapper.m_Menu; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MenuActions set) { return set.Get(); }
        public void SetCallbacks(IMenuActions instance)
        {
            if (m_Wrapper.m_MenuActionsCallbackInterface != null)
            {
                @OpenandClosemenu.started -= m_Wrapper.m_MenuActionsCallbackInterface.OnOpenandClosemenu;
                @OpenandClosemenu.performed -= m_Wrapper.m_MenuActionsCallbackInterface.OnOpenandClosemenu;
                @OpenandClosemenu.canceled -= m_Wrapper.m_MenuActionsCallbackInterface.OnOpenandClosemenu;
            }
            m_Wrapper.m_MenuActionsCallbackInterface = instance;
            if (instance != null)
            {
                @OpenandClosemenu.started += instance.OnOpenandClosemenu;
                @OpenandClosemenu.performed += instance.OnOpenandClosemenu;
                @OpenandClosemenu.canceled += instance.OnOpenandClosemenu;
            }
        }
    }
    public MenuActions @Menu => new MenuActions(this);
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCameraRotateClockwise(InputAction.CallbackContext context);
        void OnCameraRotateCounterclockwise(InputAction.CallbackContext context);
        void OnF(InputAction.CallbackContext context);
        void OnTab(InputAction.CallbackContext context);
        void OnTeleportKey(InputAction.CallbackContext context);
    }
    public interface IMenuActions
    {
        void OnOpenandClosemenu(InputAction.CallbackContext context);
    }
}
