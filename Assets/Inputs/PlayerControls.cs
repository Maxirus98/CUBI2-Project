//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Inputs/PlayerControls.inputactions
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

public partial class @PlayerControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""PlayerBase"",
            ""id"": ""c9c6a962-9da4-4d18-aac0-e2d55904caae"",
            ""actions"": [
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""fc7b7943-1e6a-4a4c-ab48-335fe44827f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""fb1a9cfa-08b3-4830-8aa6-799db4762a6b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Attack"",
                    ""type"": ""Button"",
                    ""id"": ""9a9b1577-828a-4c50-8c2c-a834ea48ec29"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Build"",
                    ""type"": ""Button"",
                    ""id"": ""abc61522-6099-4c08-bac1-6eebd8f8cf64"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ToggleGameMenu"",
                    ""type"": ""Button"",
                    ""id"": ""f2c30918-a8ed-4ec4-8907-9c3d227d366b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveCamera"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4a4f282c-1910-4e47-9e24-bde326d9053f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fec513f4-7036-4626-8c9e-65877094f769"",
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
                    ""id"": ""b7f92d03-e87d-4402-abd3-be35221525d4"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""70773a0e-0876-414b-86c7-f1674df600e6"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e346b011-945b-4512-a6d2-41cadd53a902"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""9232b1bc-0386-4230-9cfb-ccf950b62f88"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""0c3c5f08-699b-4a99-8104-52c372d44f79"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""c2b06bb5-615e-4127-b85e-12d114a4124d"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""643e4d5a-9f49-4eb5-a531-3f9a3ab4289f"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e4ad40e3-6dd6-4f86-bb81-62ea1ef9ebf7"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c1208cc6-0d57-4597-b832-7f4e580afb9a"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Build"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""595a3ee0-119e-45db-8d4f-154877b56250"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c7b56abb-e8ab-48b2-85f0-d0482d7f4eca"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attack"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4ae5d30a-d75e-4e6f-90f2-71c80f21d1a0"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleGameMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3c349c97-f502-436e-a486-c974e17b2ee8"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ToggleGameMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""aff5fc4b-4b83-4a69-b74a-636e39271fb3"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""df8f78bb-b942-4756-b773-4a5e72cad94f"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveCamera"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UiGamepad"",
            ""id"": ""3edd0f90-4871-46d3-9da8-4e54f2cb95ab"",
            ""actions"": [
                {
                    ""name"": ""Next"",
                    ""type"": ""Button"",
                    ""id"": ""740df49b-6379-43a9-8054-26072afdee96"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""02d48655-529f-45c8-86f7-f3184189cf34"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Choose"",
                    ""type"": ""Button"",
                    ""id"": ""feda8563-a6dc-4da3-9aec-8ca6921c4cc4"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""1fc7a358-4cdb-4430-9a8d-3cfaf50300a8"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Next"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""551b4d06-fc77-4aa8-8456-13161e9b42a5"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3679576d-7af6-4be1-be64-cd7b4eced334"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Choose"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""42344965-ea1e-4013-ba24-17280e161dfd"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Choose"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // PlayerBase
        m_PlayerBase = asset.FindActionMap("PlayerBase", throwIfNotFound: true);
        m_PlayerBase_Jump = m_PlayerBase.FindAction("Jump", throwIfNotFound: true);
        m_PlayerBase_Move = m_PlayerBase.FindAction("Move", throwIfNotFound: true);
        m_PlayerBase_Attack = m_PlayerBase.FindAction("Attack", throwIfNotFound: true);
        m_PlayerBase_Build = m_PlayerBase.FindAction("Build", throwIfNotFound: true);
        m_PlayerBase_ToggleGameMenu = m_PlayerBase.FindAction("ToggleGameMenu", throwIfNotFound: true);
        m_PlayerBase_MoveCamera = m_PlayerBase.FindAction("MoveCamera", throwIfNotFound: true);
        // UiGamepad
        m_UiGamepad = asset.FindActionMap("UiGamepad", throwIfNotFound: true);
        m_UiGamepad_Next = m_UiGamepad.FindAction("Next", throwIfNotFound: true);
        m_UiGamepad_Back = m_UiGamepad.FindAction("Back", throwIfNotFound: true);
        m_UiGamepad_Choose = m_UiGamepad.FindAction("Choose", throwIfNotFound: true);
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

    // PlayerBase
    private readonly InputActionMap m_PlayerBase;
    private List<IPlayerBaseActions> m_PlayerBaseActionsCallbackInterfaces = new List<IPlayerBaseActions>();
    private readonly InputAction m_PlayerBase_Jump;
    private readonly InputAction m_PlayerBase_Move;
    private readonly InputAction m_PlayerBase_Attack;
    private readonly InputAction m_PlayerBase_Build;
    private readonly InputAction m_PlayerBase_ToggleGameMenu;
    private readonly InputAction m_PlayerBase_MoveCamera;
    public struct PlayerBaseActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerBaseActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Jump => m_Wrapper.m_PlayerBase_Jump;
        public InputAction @Move => m_Wrapper.m_PlayerBase_Move;
        public InputAction @Attack => m_Wrapper.m_PlayerBase_Attack;
        public InputAction @Build => m_Wrapper.m_PlayerBase_Build;
        public InputAction @ToggleGameMenu => m_Wrapper.m_PlayerBase_ToggleGameMenu;
        public InputAction @MoveCamera => m_Wrapper.m_PlayerBase_MoveCamera;
        public InputActionMap Get() { return m_Wrapper.m_PlayerBase; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerBaseActions set) { return set.Get(); }
        public void AddCallbacks(IPlayerBaseActions instance)
        {
            if (instance == null || m_Wrapper.m_PlayerBaseActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_PlayerBaseActionsCallbackInterfaces.Add(instance);
            @Jump.started += instance.OnJump;
            @Jump.performed += instance.OnJump;
            @Jump.canceled += instance.OnJump;
            @Move.started += instance.OnMove;
            @Move.performed += instance.OnMove;
            @Move.canceled += instance.OnMove;
            @Attack.started += instance.OnAttack;
            @Attack.performed += instance.OnAttack;
            @Attack.canceled += instance.OnAttack;
            @Build.started += instance.OnBuild;
            @Build.performed += instance.OnBuild;
            @Build.canceled += instance.OnBuild;
            @ToggleGameMenu.started += instance.OnToggleGameMenu;
            @ToggleGameMenu.performed += instance.OnToggleGameMenu;
            @ToggleGameMenu.canceled += instance.OnToggleGameMenu;
            @MoveCamera.started += instance.OnMoveCamera;
            @MoveCamera.performed += instance.OnMoveCamera;
            @MoveCamera.canceled += instance.OnMoveCamera;
        }

        private void UnregisterCallbacks(IPlayerBaseActions instance)
        {
            @Jump.started -= instance.OnJump;
            @Jump.performed -= instance.OnJump;
            @Jump.canceled -= instance.OnJump;
            @Move.started -= instance.OnMove;
            @Move.performed -= instance.OnMove;
            @Move.canceled -= instance.OnMove;
            @Attack.started -= instance.OnAttack;
            @Attack.performed -= instance.OnAttack;
            @Attack.canceled -= instance.OnAttack;
            @Build.started -= instance.OnBuild;
            @Build.performed -= instance.OnBuild;
            @Build.canceled -= instance.OnBuild;
            @ToggleGameMenu.started -= instance.OnToggleGameMenu;
            @ToggleGameMenu.performed -= instance.OnToggleGameMenu;
            @ToggleGameMenu.canceled -= instance.OnToggleGameMenu;
            @MoveCamera.started -= instance.OnMoveCamera;
            @MoveCamera.performed -= instance.OnMoveCamera;
            @MoveCamera.canceled -= instance.OnMoveCamera;
        }

        public void RemoveCallbacks(IPlayerBaseActions instance)
        {
            if (m_Wrapper.m_PlayerBaseActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IPlayerBaseActions instance)
        {
            foreach (var item in m_Wrapper.m_PlayerBaseActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_PlayerBaseActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public PlayerBaseActions @PlayerBase => new PlayerBaseActions(this);

    // UiGamepad
    private readonly InputActionMap m_UiGamepad;
    private List<IUiGamepadActions> m_UiGamepadActionsCallbackInterfaces = new List<IUiGamepadActions>();
    private readonly InputAction m_UiGamepad_Next;
    private readonly InputAction m_UiGamepad_Back;
    private readonly InputAction m_UiGamepad_Choose;
    public struct UiGamepadActions
    {
        private @PlayerControls m_Wrapper;
        public UiGamepadActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Next => m_Wrapper.m_UiGamepad_Next;
        public InputAction @Back => m_Wrapper.m_UiGamepad_Back;
        public InputAction @Choose => m_Wrapper.m_UiGamepad_Choose;
        public InputActionMap Get() { return m_Wrapper.m_UiGamepad; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(UiGamepadActions set) { return set.Get(); }
        public void AddCallbacks(IUiGamepadActions instance)
        {
            if (instance == null || m_Wrapper.m_UiGamepadActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_UiGamepadActionsCallbackInterfaces.Add(instance);
            @Next.started += instance.OnNext;
            @Next.performed += instance.OnNext;
            @Next.canceled += instance.OnNext;
            @Back.started += instance.OnBack;
            @Back.performed += instance.OnBack;
            @Back.canceled += instance.OnBack;
            @Choose.started += instance.OnChoose;
            @Choose.performed += instance.OnChoose;
            @Choose.canceled += instance.OnChoose;
        }

        private void UnregisterCallbacks(IUiGamepadActions instance)
        {
            @Next.started -= instance.OnNext;
            @Next.performed -= instance.OnNext;
            @Next.canceled -= instance.OnNext;
            @Back.started -= instance.OnBack;
            @Back.performed -= instance.OnBack;
            @Back.canceled -= instance.OnBack;
            @Choose.started -= instance.OnChoose;
            @Choose.performed -= instance.OnChoose;
            @Choose.canceled -= instance.OnChoose;
        }

        public void RemoveCallbacks(IUiGamepadActions instance)
        {
            if (m_Wrapper.m_UiGamepadActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IUiGamepadActions instance)
        {
            foreach (var item in m_Wrapper.m_UiGamepadActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_UiGamepadActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public UiGamepadActions @UiGamepad => new UiGamepadActions(this);
    public interface IPlayerBaseActions
    {
        void OnJump(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
        void OnAttack(InputAction.CallbackContext context);
        void OnBuild(InputAction.CallbackContext context);
        void OnToggleGameMenu(InputAction.CallbackContext context);
        void OnMoveCamera(InputAction.CallbackContext context);
    }
    public interface IUiGamepadActions
    {
        void OnNext(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
        void OnChoose(InputAction.CallbackContext context);
    }
}
