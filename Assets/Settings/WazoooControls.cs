// GENERATED AUTOMATICALLY FROM 'Assets/Settings/WazoooControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Adhaesii.WazoooDOTexe.Settings
{
    public class @WazoooControls : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @WazoooControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""WazoooControls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""3fa1a02d-18c8-4f05-a243-809f1ab75402"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""78a2a22b-14ec-414e-a5fc-1362078bd60f"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""441e5e28-08c7-4bf5-a2c9-4a5e0294859e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Melee"",
                    ""type"": ""Button"",
                    ""id"": ""6dd05c72-fe89-41fb-9ca8-a32415116e08"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Ranged"",
                    ""type"": ""Button"",
                    ""id"": ""a6ed1586-e87d-4431-b51a-3953ff5dd7a7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hover"",
                    ""type"": ""Button"",
                    ""id"": ""0d9b0e5b-4e4a-4c2b-9f0b-1a6f344ae63d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Aim"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d7fd5e73-0eac-49a2-9da6-34b391a3de63"",
                    ""expectedControlType"": """",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""KB"",
                    ""id"": ""6ee94919-6df2-42ec-823e-73a85fcf0dbd"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""0b8cf206-f69a-43bd-9bf7-1b31aa1cfafd"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""b6843f3e-2c33-424d-a740-f30b5942748b"",
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
                    ""id"": ""af3a01d8-f19c-4863-a82c-3968b50d50cf"",
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
                    ""id"": ""2fad567c-6fd5-46f3-8241-380b17de3d55"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Melee"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9c0d7719-e700-4b70-9e0b-5b2bd5511fda"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Ranged"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0e8f55e0-a7fa-4a9d-a30c-af5bda68e995"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Hover"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b043eeeb-319f-4c43-b153-0f3b48d33c99"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Gameplay
            m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
            m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
            m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
            m_Gameplay_Melee = m_Gameplay.FindAction("Melee", throwIfNotFound: true);
            m_Gameplay_Ranged = m_Gameplay.FindAction("Ranged", throwIfNotFound: true);
            m_Gameplay_Hover = m_Gameplay.FindAction("Hover", throwIfNotFound: true);
            m_Gameplay_Aim = m_Gameplay.FindAction("Aim", throwIfNotFound: true);
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

        // Gameplay
        private readonly InputActionMap m_Gameplay;
        private IGameplayActions m_GameplayActionsCallbackInterface;
        private readonly InputAction m_Gameplay_Movement;
        private readonly InputAction m_Gameplay_Jump;
        private readonly InputAction m_Gameplay_Melee;
        private readonly InputAction m_Gameplay_Ranged;
        private readonly InputAction m_Gameplay_Hover;
        private readonly InputAction m_Gameplay_Aim;
        public struct GameplayActions
        {
            private @WazoooControls m_Wrapper;
            public GameplayActions(@WazoooControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
            public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
            public InputAction @Melee => m_Wrapper.m_Gameplay_Melee;
            public InputAction @Ranged => m_Wrapper.m_Gameplay_Ranged;
            public InputAction @Hover => m_Wrapper.m_Gameplay_Hover;
            public InputAction @Aim => m_Wrapper.m_Gameplay_Aim;
            public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
            public void SetCallbacks(IGameplayActions instance)
            {
                if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
                {
                    @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                    @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                    @Melee.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMelee;
                    @Melee.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMelee;
                    @Melee.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMelee;
                    @Ranged.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRanged;
                    @Ranged.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRanged;
                    @Ranged.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnRanged;
                    @Hover.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHover;
                    @Hover.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHover;
                    @Hover.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnHover;
                    @Aim.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                    @Aim.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                    @Aim.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAim;
                }
                m_Wrapper.m_GameplayActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Movement.started += instance.OnMovement;
                    @Movement.performed += instance.OnMovement;
                    @Movement.canceled += instance.OnMovement;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Melee.started += instance.OnMelee;
                    @Melee.performed += instance.OnMelee;
                    @Melee.canceled += instance.OnMelee;
                    @Ranged.started += instance.OnRanged;
                    @Ranged.performed += instance.OnRanged;
                    @Ranged.canceled += instance.OnRanged;
                    @Hover.started += instance.OnHover;
                    @Hover.performed += instance.OnHover;
                    @Hover.canceled += instance.OnHover;
                    @Aim.started += instance.OnAim;
                    @Aim.performed += instance.OnAim;
                    @Aim.canceled += instance.OnAim;
                }
            }
        }
        public GameplayActions @Gameplay => new GameplayActions(this);
        public interface IGameplayActions
        {
            void OnMovement(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnMelee(InputAction.CallbackContext context);
            void OnRanged(InputAction.CallbackContext context);
            void OnHover(InputAction.CallbackContext context);
            void OnAim(InputAction.CallbackContext context);
        }
    }
}
