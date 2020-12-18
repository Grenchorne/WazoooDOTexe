using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Player
{
    public class PlayerAbilityUnlockHandler : SerializedMonoBehaviour
    {
        [SerializeField]
        private bool _canJump = true;
        public bool CanJump
        {
            get => _canJump;
            set
            {
                if (value && !_canJump) OnUnlockJump?.Invoke();
                _canJump = value;
            }
        }

        [SerializeField]
        private  bool _canHover = true;
        public bool CanHover
        {
            get => _canHover;
            set
            {
                if (value && !_canHover) OnUnlockHover?.Invoke();
                _canHover = value;
            }
        }

        [SerializeField]
        private  bool _canHoverJump = true;
        public bool CanHoverJump
        {
            get => _canHoverJump;
            set
            {
                if (value && !_canHoverJump) OnUnlockHoverJump?.Invoke();
                _canHoverJump = value;
            }
        }

        [SerializeField]
        private  bool _canAttack = true;
        public bool CanAttack
        {
            get => _canAttack;
            set
            {
                if (value && !_canAttack) OnUnlockAttack?.Invoke();
                _canAttack = value;
            }
        }

        [SerializeField]
        private bool _canShoot = true;
        public bool CanShoot
        {
            get => _canShoot;
            set
            {
                if(value && !_canShoot) OnUnlockShoot?.Invoke();
                _canShoot = value;
            }
        }

        public event Action OnUnlockJump;
        public event Action OnUnlockHover;
        public event Action OnUnlockHoverJump;
        public event Action OnUnlockAttack;
        public event Action OnUnlockShoot;
    }
}
