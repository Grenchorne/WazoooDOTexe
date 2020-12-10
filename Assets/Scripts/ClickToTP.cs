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
            if (!Input.GetKeyDown(k_key)) return;

            Vector3 newPos = cam.ScreenToWorldPoint(Input.mousePosition);
            newPos.z = 0f;
            target.position = newPos;
                
            if(target.TryGetComponent(out Rigidbody2D  rigidbody2D))
                rigidbody2D.velocity = Vector2.zero;
                
            else if (target.TryGetComponent(out Rigidbody rigidbody))
                rigidbody.velocity = Vector3.zero;
        }
    }
}
