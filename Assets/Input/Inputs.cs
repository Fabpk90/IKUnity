// GENERATED AUTOMATICALLY FROM 'Assets/Input/Inputs.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class Inputs : IInputActionCollection, IDisposable
{
    private InputActionAsset asset;
    public Inputs()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Inputs"",
    ""maps"": [
        {
            ""name"": ""New action map"",
            ""id"": ""039f3a1e-5c1a-44f2-9895-cbcd04d86430"",
            ""actions"": [
                {
                    ""name"": ""LeftArm"",
                    ""type"": ""Value"",
                    ""id"": ""b7274af0-1adf-4394-aaf8-6972b03f0507"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""RightArm"",
                    ""type"": ""Value"",
                    ""id"": ""ebc53f06-3657-42ba-bfa3-bd295856f63c"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b1067527-6b79-46ed-8215-fa24cd07ceb4"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftArm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b0a8c482-cbed-4bbe-80c2-4c83bf5d625a"",
                    ""path"": ""<XInputController>/rightStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightArm"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // New action map
        m_Newactionmap = asset.FindActionMap("New action map", throwIfNotFound: true);
        m_Newactionmap_LeftArm = m_Newactionmap.FindAction("LeftArm", throwIfNotFound: true);
        m_Newactionmap_RightArm = m_Newactionmap.FindAction("RightArm", throwIfNotFound: true);
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

    // New action map
    private readonly InputActionMap m_Newactionmap;
    private INewactionmapActions m_NewactionmapActionsCallbackInterface;
    private readonly InputAction m_Newactionmap_LeftArm;
    private readonly InputAction m_Newactionmap_RightArm;
    public struct NewactionmapActions
    {
        private Inputs m_Wrapper;
        public NewactionmapActions(Inputs wrapper) { m_Wrapper = wrapper; }
        public InputAction @LeftArm => m_Wrapper.m_Newactionmap_LeftArm;
        public InputAction @RightArm => m_Wrapper.m_Newactionmap_RightArm;
        public InputActionMap Get() { return m_Wrapper.m_Newactionmap; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(NewactionmapActions set) { return set.Get(); }
        public void SetCallbacks(INewactionmapActions instance)
        {
            if (m_Wrapper.m_NewactionmapActionsCallbackInterface != null)
            {
                LeftArm.started -= m_Wrapper.m_NewactionmapActionsCallbackInterface.OnLeftArm;
                LeftArm.performed -= m_Wrapper.m_NewactionmapActionsCallbackInterface.OnLeftArm;
                LeftArm.canceled -= m_Wrapper.m_NewactionmapActionsCallbackInterface.OnLeftArm;
                RightArm.started -= m_Wrapper.m_NewactionmapActionsCallbackInterface.OnRightArm;
                RightArm.performed -= m_Wrapper.m_NewactionmapActionsCallbackInterface.OnRightArm;
                RightArm.canceled -= m_Wrapper.m_NewactionmapActionsCallbackInterface.OnRightArm;
            }
            m_Wrapper.m_NewactionmapActionsCallbackInterface = instance;
            if (instance != null)
            {
                LeftArm.started += instance.OnLeftArm;
                LeftArm.performed += instance.OnLeftArm;
                LeftArm.canceled += instance.OnLeftArm;
                RightArm.started += instance.OnRightArm;
                RightArm.performed += instance.OnRightArm;
                RightArm.canceled += instance.OnRightArm;
            }
        }
    }
    public NewactionmapActions @Newactionmap => new NewactionmapActions(this);
    public interface INewactionmapActions
    {
        void OnLeftArm(InputAction.CallbackContext context);
        void OnRightArm(InputAction.CallbackContext context);
    }
}
