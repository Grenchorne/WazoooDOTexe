using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class SwordController : SerializedMonoBehaviour
    {
        [SerializeField] private string attackTrigger = "Swing";
        [SerializeField] private float cooldown = 0.27f;

        public event Action OnSwing;

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

            OnSwing?.Invoke();
            t_cooldown = cooldown;
            anim.SetTrigger(h_attack);
        }

        private void Update()
        {
            t_cooldown -= Time.deltaTime;
        }
    }
}
