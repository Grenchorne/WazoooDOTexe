using System;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.WazoooInput.MonoBehaviours
{
    public class PlayerInput : InputBase
    {
        private static PlayerInput __instance;
        public static PlayerInput Instance => __instance;

        public InputButton Pause = new InputButton(KeyCode.Escape, XboxControllerButtons.Menu);
        public InputButton Jump = new InputButton(KeyCode.Space, XboxControllerButtons.A);
        public InputButton Hover = new InputButton(KeyCode.LeftShift, XboxControllerButtons.LeftBumper);
        public InputButton MeleeAttack = new InputButton(KeyCode.J, XboxControllerButtons.X);
        public InputButton RangedAttack = new InputButton(KeyCode.K, XboxControllerButtons.B);
        public InputAxis Horizontal = new InputAxis(KeyCode.D, KeyCode.A, XboxControllerAxes.LeftStickHorizontal);
        public InputAxis Vertical = new InputAxis(KeyCode.W, KeyCode.S, XboxControllerAxes.LeftStickVertical);

        private InputButton[] buttons;
        private InputAxis[] axes;
        
        private bool _haveControl = true;
        public bool HaveControl => _haveControl;

        private void Awake()
        {
            if (__instance == null)
                __instance = this;
            else
                throw new UnityException("Only one instance of PlayerInput allowed");

            buttons = new[]
            {
                Pause, Jump, Hover, MeleeAttack, RangedAttack
            };

            axes = new[]
            {
                Horizontal, Vertical
            };
        }

        private void OnEnable()
        {
            if (__instance == null)
                __instance = this;
            else if (__instance != this)
                throw new UnityException($"Only one instance of PlayerInput allowed: {name}; {__instance.name}");
        }

        private void OnDisable() => __instance = null;

        protected override void GetInputs(bool fixedUpdateHappened)
        {
            foreach (InputButton btn in buttons)
                btn.Get(fixedUpdateHappened, inputType);

            foreach (InputAxis axis in axes)
                axis.Get(inputType);
        }

        public override void GainControl()
        {
            _haveControl = true;
            
            foreach (InputButton btn in buttons)
                btn.GainControl();

            foreach (InputAxis axis in axes)
                axis.GainControl();
        }

        public override void ReleaseControl(bool resetValues = true)
        {
            _haveControl = false;

            foreach (InputButton btn in buttons)
                StartCoroutine(btn.ReleaseControl(resetValues));

            foreach (InputAxis axis in axes)
                axis.ReleaseControl(resetValues);
        }

        public void DisableMelee() => MeleeAttack.Disable();
        public void EnableMelee() => MeleeAttack.Enable();

        public void DisableRanged() => RangedAttack.Disable();
        public void EnableRanged() => RangedAttack.Enable();

        public void DisableHover() => Hover.Disable();
        public void EnableHover() => Hover.Enable();
    }
}