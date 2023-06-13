//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/_Source/PlayerSystem/Input/PlayerInput.inputactions
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
            ""name"": ""Action"",
            ""id"": ""40cfe7f0-8ea0-4eb3-bfaa-02162de8664c"",
            ""actions"": [
                {
                    ""name"": ""MoveZ"",
                    ""type"": ""Button"",
                    ""id"": ""77cf35a9-c985-4c52-901a-ff31c3b4d1fa"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MoveX"",
                    ""type"": ""Button"",
                    ""id"": ""b55335ca-7f48-4a99-9389-84e3a86f7954"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""16cccb05-1c00-489a-bf9d-3b586cc6d321"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Squat"",
                    ""type"": ""Button"",
                    ""id"": ""fccca7ac-f25f-4140-99a6-0f2dfd8733cc"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""SeletionItem"",
                    ""type"": ""Button"",
                    ""id"": ""c649d1b7-6959-46cf-b759-4f3132c10b43"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""DropItem"",
                    ""type"": ""Button"",
                    ""id"": ""3bcc158a-c0b6-42f4-b1d7-867b6c23592d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""UseItem"",
                    ""type"": ""Button"",
                    ""id"": ""0ecb6d3d-6ee8-4430-be85-5d3b4017c351"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""f8f39528-743e-40e0-97f1-de6181da07a6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""Move"",
                    ""id"": ""41738e85-c7eb-4adc-bde3-3603ec9657c4"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveZ"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""2c5f91f0-ab53-40d7-9440-7e032de78722"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveZ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""fb026ef6-67b1-4ade-a958-bb38fef827dc"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveZ"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Move"",
                    ""id"": ""c3e0b823-d753-4017-9c6f-575912fbd7b5"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveX"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e942c01c-d58c-4b67-8fd3-48f2bc2c8dee"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""bbeb3c28-91a8-4c5d-859b-b428de5d4439"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MoveX"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""de7f748e-b9b4-4d28-bbb6-307c6a20821c"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b1ba04d-bb84-4894-8550-d663945da86e"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Squat"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""dd672dcb-af91-4707-b8fb-c4e2363722d9"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SeletionItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ffe56960-14ac-4971-a23d-51fe1df8e41c"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""DropItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e5dfbca5-fe8c-4a46-9e8a-7adbfe70f4fb"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""UseItem"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""83c9a12d-4b63-4d8b-ab71-1b4cfe4cd44a"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Action
        m_Action = asset.FindActionMap("Action", throwIfNotFound: true);
        m_Action_MoveZ = m_Action.FindAction("MoveZ", throwIfNotFound: true);
        m_Action_MoveX = m_Action.FindAction("MoveX", throwIfNotFound: true);
        m_Action_Run = m_Action.FindAction("Run", throwIfNotFound: true);
        m_Action_Squat = m_Action.FindAction("Squat", throwIfNotFound: true);
        m_Action_SeletionItem = m_Action.FindAction("SeletionItem", throwIfNotFound: true);
        m_Action_DropItem = m_Action.FindAction("DropItem", throwIfNotFound: true);
        m_Action_UseItem = m_Action.FindAction("UseItem", throwIfNotFound: true);
        m_Action_Pause = m_Action.FindAction("Pause", throwIfNotFound: true);
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

    // Action
    private readonly InputActionMap m_Action;
    private IActionActions m_ActionActionsCallbackInterface;
    private readonly InputAction m_Action_MoveZ;
    private readonly InputAction m_Action_MoveX;
    private readonly InputAction m_Action_Run;
    private readonly InputAction m_Action_Squat;
    private readonly InputAction m_Action_SeletionItem;
    private readonly InputAction m_Action_DropItem;
    private readonly InputAction m_Action_UseItem;
    private readonly InputAction m_Action_Pause;
    public struct ActionActions
    {
        private @PlayerInput m_Wrapper;
        public ActionActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @MoveZ => m_Wrapper.m_Action_MoveZ;
        public InputAction @MoveX => m_Wrapper.m_Action_MoveX;
        public InputAction @Run => m_Wrapper.m_Action_Run;
        public InputAction @Squat => m_Wrapper.m_Action_Squat;
        public InputAction @SeletionItem => m_Wrapper.m_Action_SeletionItem;
        public InputAction @DropItem => m_Wrapper.m_Action_DropItem;
        public InputAction @UseItem => m_Wrapper.m_Action_UseItem;
        public InputAction @Pause => m_Wrapper.m_Action_Pause;
        public InputActionMap Get() { return m_Wrapper.m_Action; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionActions set) { return set.Get(); }
        public void SetCallbacks(IActionActions instance)
        {
            if (m_Wrapper.m_ActionActionsCallbackInterface != null)
            {
                @MoveZ.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnMoveZ;
                @MoveZ.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnMoveZ;
                @MoveZ.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnMoveZ;
                @MoveX.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnMoveX;
                @MoveX.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnMoveX;
                @MoveX.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnMoveX;
                @Run.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnRun;
                @Squat.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnSquat;
                @Squat.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnSquat;
                @Squat.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnSquat;
                @SeletionItem.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnSeletionItem;
                @SeletionItem.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnSeletionItem;
                @SeletionItem.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnSeletionItem;
                @DropItem.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnDropItem;
                @DropItem.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnDropItem;
                @DropItem.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnDropItem;
                @UseItem.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnUseItem;
                @UseItem.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnUseItem;
                @UseItem.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnUseItem;
                @Pause.started -= m_Wrapper.m_ActionActionsCallbackInterface.OnPause;
                @Pause.performed -= m_Wrapper.m_ActionActionsCallbackInterface.OnPause;
                @Pause.canceled -= m_Wrapper.m_ActionActionsCallbackInterface.OnPause;
            }
            m_Wrapper.m_ActionActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MoveZ.started += instance.OnMoveZ;
                @MoveZ.performed += instance.OnMoveZ;
                @MoveZ.canceled += instance.OnMoveZ;
                @MoveX.started += instance.OnMoveX;
                @MoveX.performed += instance.OnMoveX;
                @MoveX.canceled += instance.OnMoveX;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Squat.started += instance.OnSquat;
                @Squat.performed += instance.OnSquat;
                @Squat.canceled += instance.OnSquat;
                @SeletionItem.started += instance.OnSeletionItem;
                @SeletionItem.performed += instance.OnSeletionItem;
                @SeletionItem.canceled += instance.OnSeletionItem;
                @DropItem.started += instance.OnDropItem;
                @DropItem.performed += instance.OnDropItem;
                @DropItem.canceled += instance.OnDropItem;
                @UseItem.started += instance.OnUseItem;
                @UseItem.performed += instance.OnUseItem;
                @UseItem.canceled += instance.OnUseItem;
                @Pause.started += instance.OnPause;
                @Pause.performed += instance.OnPause;
                @Pause.canceled += instance.OnPause;
            }
        }
    }
    public ActionActions @Action => new ActionActions(this);
    public interface IActionActions
    {
        void OnMoveZ(InputAction.CallbackContext context);
        void OnMoveX(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnSquat(InputAction.CallbackContext context);
        void OnSeletionItem(InputAction.CallbackContext context);
        void OnDropItem(InputAction.CallbackContext context);
        void OnUseItem(InputAction.CallbackContext context);
        void OnPause(InputAction.CallbackContext context);
    }
}
