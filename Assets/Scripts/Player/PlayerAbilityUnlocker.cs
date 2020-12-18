using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Adhaesii.WazoooDOTexe.Player
{
    public class PlayerAbilityUnlocker : SerializedMonoBehaviour
    {
        private enum Ability
        {
            Jump,
            Attack,
            Hover,
            HoverJump
        }

        [SerializeField]
        private Ability ability;

        [SerializeField]
        private string playerTag = "Player";

        [SerializeField, HideInInspector]
        private bool _useTrigger;

        [ShowInInspector]
        private bool UseTrigger
        {
            get => _useTrigger;
            set
            {
                if (value)
                {
                    if (GetComponent<Collider2D>() == null)
                    {
                        BoxCollider2D c = gameObject.AddComponent<BoxCollider2D>();
                        c.isTrigger = true;
                    }
                }
                _useTrigger = value;
            }
        }

        public UnityEvent OnUnlock; 

        public void UnlockAbility()
        {
            var ab = FindObjectOfType<PlayerAbilityUnlockHandler>();

            switch (ability)
            {
                case Ability.Jump:
                    if (!ab.CanJump)
                    {
                        ab.CanJump = true;
                        OnUnlock?.Invoke();
                    }

                    break;
                case Ability.Attack:
                    if (!ab.CanAttack)
                    {
                        ab.CanAttack = true;
                        OnUnlock?.Invoke();
                    }

                    break;
                case Ability.Hover:
                    if (!ab.CanHover)
                    {
                        ab.CanHover = true;
                        OnUnlock?.Invoke();
                    }

                    break;
                case Ability.HoverJump:
                    if (!ab.CanHoverJump)
                    {
                        ab.CanHoverJump = true;
                        OnUnlock?.Invoke();
                    }

                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.CompareTag(playerTag))
                return;
            
            UnlockAbility();
            
            gameObject.SetActive(false);
        }
    }
}
