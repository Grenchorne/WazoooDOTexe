using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.UI
{
    public class MessageNotification : SerializedMonoBehaviour
    {
        public void Show() => LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 1, .75f);

        public void Hide() => LeanTween.alphaCanvas(GetComponent<CanvasGroup>(), 0, .75f);
    }
}
