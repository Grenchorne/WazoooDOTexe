using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
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

        public event Action OnUnlock;

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
    }
}
