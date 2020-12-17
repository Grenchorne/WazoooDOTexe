using System.Linq;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    [RequireComponent(typeof(Collider2D))]
    public class DisableOnTrigger : SerializedMonoBehaviour
    {
        [SerializeField]
        private string[] tags = {"Player"};

        [SerializeField]
        private bool useSelf = true;

        [SerializeField, HideIf("useSelf")]
        private GameObject target;

        private void OnTriggerEnter2D(Collider2D other)
        {
            // If any of the tags on the other collider match to the ones the designer has specified,
            // disable the gameObject, which is either this or specified in-editor
            if (tags.Any(other.CompareTag)) (useSelf ? gameObject : target).SetActive(false);
        }
    }
}
