using System;
using System.Collections.Generic;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.WazoooInput
{
    /// <summary>
    /// This is blatantly taken from the Unity 2D Game Kit -- it's good enough for me
    /// </summary>
    [Serializable]
    public class InputAxis
    {
        private static readonly Dictionary<int, string> k_axisToName = new Dictionary<int, string>
        {
            {(int)XboxControllerAxes.LeftStickHorizontal, "Leftstick Horizontal"},
            {(int)XboxControllerAxes.LeftStickVertical, "Leftstick Vertical"},
            {(int)XboxControllerAxes.DpadHorizontal, "Dpad Horizontal"},
            {(int)XboxControllerAxes.DpadVertical, "Dpad Vertical"},
            {(int)XboxControllerAxes.RightStickHorizontal, "Rightstick Horizontal"},
            {(int)XboxControllerAxes.RightStickVertical, "Rightstick Vertical"},
            {(int)XboxControllerAxes.LeftTrigger, "Left Trigger"},
            {(int)XboxControllerAxes.RightTrigger, "Right Trigger"},
        };
        
        public KeyCode positive;
        public KeyCode negative;
        public XboxControllerAxes controllerAxis;
        
        public float Value { get; private set; }
        public bool ReceivingInput { get; private set; }

        [SerializeField]
        private bool _enabled = true;
        public bool Enabled => _enabled;

        private bool gettingInput = true;


        public InputAxis(KeyCode positive, KeyCode negative, XboxControllerAxes controllerAxis)
        {
            this.positive = positive;
            this.negative = negative;
            this.controllerAxis = controllerAxis;
        }

        public void Get(InputType inputType)
        {
            if (!Enabled)
            {
                Value = 0f;
                return;
            }

            if (!gettingInput)
                return;

            bool positiveHeld = false;
            bool negativeHeld = false;

            if (inputType == InputType.Controller)
            {
                float value = Input.GetAxisRaw(k_axisToName[(int) controllerAxis]);
                positiveHeld = value > Single.Epsilon;
                negativeHeld = value < -Single.Epsilon;
            }
            
            else if (inputType == InputType.MouseAndKeyboard)
            {
                positiveHeld = Input.GetKey(positive);
                negativeHeld = Input.GetKey(negative);
            }

            if (positiveHeld == negativeHeld)
                Value = 0f;
            else if (positiveHeld)
                Value = 1f;
            else
                Value = -1;

            ReceivingInput = positiveHeld | negativeHeld;
        }

        public void Enable() => _enabled = true;
        public void Disable() => _enabled = false;

        public void GainControl() => gettingInput = true;

        public void ReleaseControl(bool resetValues)
        {
            gettingInput = false;
            if (!resetValues) return;
            Value = 0f;
            ReceivingInput = false;
        }

    }
}