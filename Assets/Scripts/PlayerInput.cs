using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class PlayerInput : SerializedMonoBehaviour
    {
        [SerializeField]
        private string horizontalAxis = "Horizontal";

        [SerializeField]
        private string jumpAxis = "Jump";

        [SerializeField]
        private string attackAxis = "Fire1";

        [SerializeField]
        private string hoverAxis = "Fire3";

        [SerializeField]
        private string peekAxis = "Vertical";
        
        public float Horizontal { get; private set; }
        public bool Jump { get; private set; }
        public bool Attack { get; private set; }
        public bool Hover { get; private set; }
        
        public float Peek { get; private set; }
        
        public bool NoInput { get; private set; }
        public float TimeSinceLastInput { get; private set; }

        private void Update()
        {
            Horizontal = Input.GetAxisRaw(horizontalAxis);
            Jump = Input.GetAxis(jumpAxis) > 0;
            Attack = Input.GetAxis(attackAxis) > 0;
            //Attack = Input.GetMouseButtonDown(0);
            Hover = Input.GetAxis(hoverAxis) > 0;
            Peek = Input.GetAxis(peekAxis);

            NoInput = Mathf.Approximately(Horizontal, 0) && !Jump && !Attack && !Hover && Mathf.Approximately(Peek, 0);

            TimeSinceLastInput = NoInput ? TimeSinceLastInput + Time.deltaTime : 0f; 
        }
    }
}