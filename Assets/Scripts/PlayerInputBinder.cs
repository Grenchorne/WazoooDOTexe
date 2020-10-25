using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Adhaesii.WazoooDOTexe
{
    
    public class PlayerInputBinder : SerializedMonoBehaviour
    {
        [SerializeField] private InputActionAsset playerInput;
        
        private CharacterMover _mover;
        InputAction movement, jump, melee, ranged, hover, aim;
        
        private void Awake()
        {
            _mover = GetComponent<CharacterMover>();
            InputActionMap gameplay = playerInput.FindActionMap("Gameplay");

            movement = gameplay.FindAction("Movement");
            jump = gameplay.FindAction("Jump");
            melee = gameplay.FindAction("Melee");
            ranged = gameplay.FindAction("Ranged");
            hover = gameplay.FindAction("Hover");
            aim = gameplay.FindAction("Aim");
            
            movement.performed += context => _mover.Move(context.ReadValue<float>());
            jump.performed += _ => _mover.Jump();
            melee.performed += _ => _mover.Melee();
            hover.performed += _ => _mover.Hover();
            hover.canceled += _ => _mover.EndHover();
        }

        private void OnEnable()
        {
            movement.Enable();
            jump.Enable();
            melee.Enable();
            hover.Enable();
        }

        private void OnDisable()
        {
            movement.Disable();
            jump.Disable();
            melee.Disable();
            hover.Disable();
        }
    }
}
