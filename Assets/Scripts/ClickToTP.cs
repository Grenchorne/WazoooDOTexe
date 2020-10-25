using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Adhaesii.WazoooDOTexe
{
    public class ClickToTP : SerializedMonoBehaviour
    {
        [SerializeField] private Transform target;
        
        private Camera cam;
        private Mouse mouse;
        private Keyboard kb;

        private void Awake()
        {
            cam = GetComponent<Camera>();
            mouse = Mouse.current;
            kb = Keyboard.current;
        }

        private bool tp;
        private void Update()
        {
            if (kb.zKey.isPressed && !tp)
            {
                tp = true;
                target.position = cam.ScreenToWorldPoint(mouse.position.ReadValue());
            }
            else
            {
                tp = false;
            }
        }
    }
}
