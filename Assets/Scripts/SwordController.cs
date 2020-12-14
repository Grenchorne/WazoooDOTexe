using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class SwordController : SerializedMonoBehaviour
    {
        [SerializeField] private string attackTrigger = "Swing";
        [SerializeField] private float cooldown = 0.27f;

        [SerializeField]
        private Transform handNode;

        [SerializeField]
        private Vector3 offset;

        public event Action OnSwing;

        private Animator anim;

        private float t_cooldown;
        private int h_attack;


        private Transform thisTransform;
        private Transform parentTransform;

        private void Awake()
        {
            anim = GetComponent<Animator>();

            thisTransform = transform;
            parentTransform = thisTransform.parent;
            h_attack = Animator.StringToHash(attackTrigger);
        }
        private void Update()
        {
            t_cooldown -= Time.deltaTime;
        }

        private void LateUpdate()
        {
            // track the hand position - note that the sprite is flipped based on direction, so we inverse the offset 
            // under those circumstances
            Vector2 handNodPos = handNode.position;
            thisTransform.position = new Vector3(handNodPos.x + offset.x * (parentTransform.localScale.x > 0 ? 1 : -1),
                handNodPos.y + offset.y);
        }

        public void Attack()
        {
            if (t_cooldown > 0)
                return;

            OnSwing?.Invoke();
            t_cooldown = cooldown;
            anim.SetTrigger(h_attack);
        }

    }
}
