using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.UI
{
    public class CursorDisplay : SerializedMonoBehaviour
    {
        private void Start() => Cursor.lockState = CursorLockMode.Confined;

        public void ShowCursor() => Cursor.visible = true;
        public void HideCursor() => Cursor.visible = false;
    }
}
