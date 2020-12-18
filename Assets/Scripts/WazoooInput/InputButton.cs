using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.WazoooInput
{
    /// <summary>
    /// This is blatantly taken from the Unity 2D Game Kit -- it's good enough for me
    /// </summary>
    [System.Serializable]
    public class InputButton
    {
        private static readonly Dictionary<int, string> k_buttonsToName = new Dictionary<int, string>()
        {
            {(int)XboxControllerButtons.A, "A"},
            {(int)XboxControllerButtons.B, "B"},
            {(int)XboxControllerButtons.X, "X"},
            {(int)XboxControllerButtons.Y, "Y"},
            {(int)XboxControllerButtons.LeftStick, "Leftstick"},
            {(int)XboxControllerButtons.RightStick, "Rightstick"},
            {(int)XboxControllerButtons.View, "View"},
            {(int)XboxControllerButtons.Menu, "Menu"},
            {(int)XboxControllerButtons.LeftBumper, "Left Bumper"},
            {(int)XboxControllerButtons.RightBumper, "Right Bumper"},
        };
        
        public KeyCode key;
        public XboxControllerButtons controllerButton;
        
        public bool Down { get; private set; }
        public bool Held { get; private set; }
        public bool Up { get; private set; }

        [SerializeField]
        private bool _enabled = true;
        public bool Enabled => _enabled;

        private bool gettingInput = true;

        private bool afterFixedUpdateDown;
        private bool afterFixedUpdateHeld;
        private bool afterFixedUpdateUp;

        public InputButton(KeyCode key, XboxControllerButtons controllerButton)
        {
            this.key = key;
            this.controllerButton = controllerButton;
        }

        public void Get(bool fixedUpdateHappened, InputType inputType)
        {
            if (!Enabled)
            {
                Down = false;
                Held = false;
                Up = false;
                return;
            }
            
            if(!gettingInput)
                return;

            if (inputType == InputType.Controller)
            {
                if (fixedUpdateHappened)
                {
                    afterFixedUpdateDown = Down = Input.GetButtonDown(k_buttonsToName[(int) controllerButton]);
                    afterFixedUpdateHeld = Held = Input.GetButton(k_buttonsToName[(int) controllerButton]);
                    afterFixedUpdateUp = Up = Input.GetButtonUp(k_buttonsToName[(int) controllerButton]);
                }

                else
                {
                    afterFixedUpdateDown |= Down = Input.GetButtonDown(k_buttonsToName[(int) controllerButton]) ||
                                                   afterFixedUpdateDown;
                    afterFixedUpdateHeld |=
                        Held = Input.GetButton(k_buttonsToName[(int) controllerButton]) || afterFixedUpdateHeld;
                    afterFixedUpdateUp |= Up = Input.GetButtonUp(k_buttonsToName[(int) controllerButton]) ||
                                               afterFixedUpdateUp;
                }
            }
            
            else if (inputType == InputType.MouseAndKeyboard)
            {
                if (fixedUpdateHappened)
                {
                    afterFixedUpdateDown = Down = Input.GetKeyDown(key);
                    afterFixedUpdateHeld = Held = Input.GetKey(key);
                    afterFixedUpdateUp = Up = Input.GetKeyUp(key);
                }

                else
                {
                    afterFixedUpdateDown |= Down = Input.GetKeyDown(key);
                    afterFixedUpdateHeld |= Held = Input.GetKey(key);
                    afterFixedUpdateUp |= Up = Input.GetKeyUp(key);
                }
            }
        }

        public void Enable() => _enabled = true;
        public void Disable() => _enabled = false;

        public void GainControl() => gettingInput = true;

        public IEnumerator ReleaseControl(bool resetValues)
        {
            gettingInput = false;
            
            if(!resetValues)
                yield break;

            if (Down)
                Up = true;
            Down = false;
            Held = false;

            afterFixedUpdateUp = afterFixedUpdateHeld = afterFixedUpdateDown = false;
            
            yield return null;

            Up = false;
        }

    }
}