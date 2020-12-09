using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class ClickToTP : SerializedMonoBehaviour
    {
        [SerializeField] private Transform target;

        [SerializeField]
        private KeyCode k_key = KeyCode.Z;
        
        private Camera cam;

        private void Awake()
        {
            cam = GetComponent<Camera>();
        }

        private bool tp;
        private void Update()
        {
            if (Input.GetKeyDown(k_key)) target.position = cam.ScreenToWorldPoint(Input.mousePosition);
        }
    }
}
