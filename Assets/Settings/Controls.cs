// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Camera"",
            ""id"": ""6130ff93-80f4-4b74-8e0d-144af8cce62c"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""37f69af1-c723-44ee-9b50-fe36574421f9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Start Movement"",
                    ""type"": ""Button"",
                    ""id"": ""3c29f367-3448-49bc-a7d9-1eb56a1d8640"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""42c1fab9-3d73-47ba-9d9c-27fcf311735c"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1d5e4422-d992-48a5-8d98-016e1dc0d770"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Start Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Debug"",
            ""id"": ""dde9b440-4407-4aed-8573-0cdbdeffa437"",
            ""actions"": [
                {
                    ""name"": ""Add Block"",
                    ""type"": ""Button"",
                    ""id"": ""c70162ef-e91f-4333-bdc5-3d530d093f49"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Remove Block"",
                    ""type"": ""Button"",
                    ""id"": ""44754cf0-195b-42ac-a3ad-4f813824a01a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move Block Up"",
                    ""type"": ""Button"",
                    ""id"": ""6e82c48b-4f12-4595-b45c-cc9fd1dad691"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move Block Down"",
                    ""type"": ""Button"",
                    ""id"": ""2515d714-0f73-4d38-a091-e9be98c3ae4a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""19e6148e-1d07-4ada-9d20-5f82f6b08117"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Add Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""79571ab7-1e99-496b-9d3d-1ba1780bd782"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Remove Block"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""56aa1256-ba77-4bbe-82f5-f9ef03878a4a"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Block Up"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b5e69572-fbf2-483c-bab1-c4d5b13169cb"",
                    ""path"": ""<Keyboard>/downArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move Block Down"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Global"",
            ""id"": ""fb14ac01-a28f-429e-b117-afb2123cf7f1"",
            ""actions"": [
                {
                    ""name"": ""Mouse Position"",
                    ""type"": ""Value"",
                    ""id"": ""e42511bd-543a-4113-9087-778ac3a6a728"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""78b7b2a3-46fb-4f46-9147-89dd2b591232"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Camera
        m_Camera = asset.FindActionMap("Camera", throwIfNotFound: true);
        m_Camera_Move = m_Camera.FindAction("Move", throwIfNotFound: true);
        m_Camera_StartMovement = m_Camera.FindAction("Start Movement", throwIfNotFound: true);
        // Debug
        m_Debug = asset.FindActionMap("Debug", throwIfNotFound: true);
        m_Debug_AddBlock = m_Debug.FindAction("Add Block", throwIfNotFound: true);
        m_Debug_RemoveBlock = m_Debug.FindAction("Remove Block", throwIfNotFound: true);
        m_Debug_MoveBlockUp = m_Debug.FindAction("Move Block Up", throwIfNotFound: true);
        m_Debug_MoveBlockDown = m_Debug.FindAction("Move Block Down", throwIfNotFound: true);
        // Global
        m_Global = asset.FindActionMap("Global", throwIfNotFound: true);
        m_Global_MousePosition = m_Global.FindAction("Mouse Position", throwIfNotFound: true);
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

    // Camera
    private readonly InputActionMap m_Camera;
    private ICameraActions m_CameraActionsCallbackInterface;
    private readonly InputAction m_Camera_Move;
    private readonly InputAction m_Camera_StartMovement;
    public struct CameraActions
    {
        private @Controls m_Wrapper;
        public CameraActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Camera_Move;
        public InputAction @StartMovement => m_Wrapper.m_Camera_StartMovement;
        public InputActionMap Get() { return m_Wrapper.m_Camera; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CameraActions set) { return set.Get(); }
        public void SetCallbacks(ICameraActions instance)
        {
            if (m_Wrapper.m_CameraActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnMove;
                @StartMovement.started -= m_Wrapper.m_CameraActionsCallbackInterface.OnStartMovement;
                @StartMovement.performed -= m_Wrapper.m_CameraActionsCallbackInterface.OnStartMovement;
                @StartMovement.canceled -= m_Wrapper.m_CameraActionsCallbackInterface.OnStartMovement;
            }
            m_Wrapper.m_CameraActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @StartMovement.started += instance.OnStartMovement;
                @StartMovement.performed += instance.OnStartMovement;
                @StartMovement.canceled += instance.OnStartMovement;
            }
        }
    }
    public CameraActions @Camera => new CameraActions(this);

    // Debug
    private readonly InputActionMap m_Debug;
    private IDebugActions m_DebugActionsCallbackInterface;
    private readonly InputAction m_Debug_AddBlock;
    private readonly InputAction m_Debug_RemoveBlock;
    private readonly InputAction m_Debug_MoveBlockUp;
    private readonly InputAction m_Debug_MoveBlockDown;
    public struct DebugActions
    {
        private @Controls m_Wrapper;
        public DebugActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @AddBlock => m_Wrapper.m_Debug_AddBlock;
        public InputAction @RemoveBlock => m_Wrapper.m_Debug_RemoveBlock;
        public InputAction @MoveBlockUp => m_Wrapper.m_Debug_MoveBlockUp;
        public InputAction @MoveBlockDown => m_Wrapper.m_Debug_MoveBlockDown;
        public InputActionMap Get() { return m_Wrapper.m_Debug; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DebugActions set) { return set.Get(); }
        public void SetCallbacks(IDebugActions instance)
        {
            if (m_Wrapper.m_DebugActionsCallbackInterface != null)
            {
                @AddBlock.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnAddBlock;
                @AddBlock.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnAddBlock;
                @AddBlock.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnAddBlock;
                @RemoveBlock.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnRemoveBlock;
                @RemoveBlock.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnRemoveBlock;
                @RemoveBlock.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnRemoveBlock;
                @MoveBlockUp.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnMoveBlockUp;
                @MoveBlockUp.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnMoveBlockUp;
                @MoveBlockUp.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnMoveBlockUp;
                @MoveBlockDown.started -= m_Wrapper.m_DebugActionsCallbackInterface.OnMoveBlockDown;
                @MoveBlockDown.performed -= m_Wrapper.m_DebugActionsCallbackInterface.OnMoveBlockDown;
                @MoveBlockDown.canceled -= m_Wrapper.m_DebugActionsCallbackInterface.OnMoveBlockDown;
            }
            m_Wrapper.m_DebugActionsCallbackInterface = instance;
            if (instance != null)
            {
                @AddBlock.started += instance.OnAddBlock;
                @AddBlock.performed += instance.OnAddBlock;
                @AddBlock.canceled += instance.OnAddBlock;
                @RemoveBlock.started += instance.OnRemoveBlock;
                @RemoveBlock.performed += instance.OnRemoveBlock;
                @RemoveBlock.canceled += instance.OnRemoveBlock;
                @MoveBlockUp.started += instance.OnMoveBlockUp;
                @MoveBlockUp.performed += instance.OnMoveBlockUp;
                @MoveBlockUp.canceled += instance.OnMoveBlockUp;
                @MoveBlockDown.started += instance.OnMoveBlockDown;
                @MoveBlockDown.performed += instance.OnMoveBlockDown;
                @MoveBlockDown.canceled += instance.OnMoveBlockDown;
            }
        }
    }
    public DebugActions @Debug => new DebugActions(this);

    // Global
    private readonly InputActionMap m_Global;
    private IGlobalActions m_GlobalActionsCallbackInterface;
    private readonly InputAction m_Global_MousePosition;
    public struct GlobalActions
    {
        private @Controls m_Wrapper;
        public GlobalActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_Global_MousePosition;
        public InputActionMap Get() { return m_Wrapper.m_Global; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GlobalActions set) { return set.Get(); }
        public void SetCallbacks(IGlobalActions instance)
        {
            if (m_Wrapper.m_GlobalActionsCallbackInterface != null)
            {
                @MousePosition.started -= m_Wrapper.m_GlobalActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_GlobalActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_GlobalActionsCallbackInterface.OnMousePosition;
            }
            m_Wrapper.m_GlobalActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
            }
        }
    }
    public GlobalActions @Global => new GlobalActions(this);
    public interface ICameraActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnStartMovement(InputAction.CallbackContext context);
    }
    public interface IDebugActions
    {
        void OnAddBlock(InputAction.CallbackContext context);
        void OnRemoveBlock(InputAction.CallbackContext context);
        void OnMoveBlockUp(InputAction.CallbackContext context);
        void OnMoveBlockDown(InputAction.CallbackContext context);
    }
    public interface IGlobalActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
    }
}
