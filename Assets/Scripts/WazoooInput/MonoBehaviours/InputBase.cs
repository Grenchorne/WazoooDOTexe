using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.WazoooInput.MonoBehaviours
{
    public abstract class InputBase : SerializedMonoBehaviour
    {
        public InputType inputType = InputType.MouseAndKeyboard;
        private bool fixedUpdateHappened;

        private void Update()
        {
            GetInputs(fixedUpdateHappened || Mathf.Approximately(Time.timeScale, 0));
            fixedUpdateHappened = false;
        }

        private void FixedUpdate() => fixedUpdateHappened = true;

        protected abstract void GetInputs(bool fixedUpdateHappened);
        
        public abstract void GainControl();

        public abstract void ReleaseControl(bool resetValues = true);

        protected void ReleaseControl(InputButton inputButton, bool resetValues) =>
            StartCoroutine(inputButton.ReleaseControl(resetValues));

        protected void ReleaseControl(InputAxis inputAxis, bool resetValues) =>
            inputAxis.ReleaseControl(resetValues);

    }
}