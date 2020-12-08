using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Old
{
    public class SwordController : SerializedMonoBehaviour
    {
        [SerializeField] private string attackTrigger = "New Trigger";
        [SerializeField] private float cooldown = 0.27f;

        private Animator anim;

        private float t_cooldown;
        private int h_attack;

        private void Awake()
        {
            anim = GetComponent<Animator>();
            h_attack = Animator.StringToHash(attackTrigger);
        }

        public void Attack()
        {
            if (t_cooldown > 0)
                return;

            t_cooldown = cooldown;
            anim.SetTrigger(h_attack);
        }

        private void Update()
        {
            t_cooldown -= Time.deltaTime;
        }
    }
}
