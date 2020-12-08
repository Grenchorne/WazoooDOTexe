using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Old
{
    public class Breakable : SerializedMonoBehaviour, IReceiveDamage
    {
        [SerializeField] private GameObject breakFX;
        public void Damage()
        {
            if (breakFX)
            {
                GameObject breakInstance = Instantiate(breakFX);
                breakInstance.transform.position = transform.position;
            }
            Destroy(gameObject);
        }
    }
}