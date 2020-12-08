using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class FuelHandler : SerializedMonoBehaviour
    {
        [SerializeField] private float groundedRechargeRate = 1f;
        [SerializeField] private float depletionRate = 0.01f;
        [SerializeField] private int pips = 4;

        private float _fuel = 1f;
        public event Action OnDeplete;

        [ShowInInspector]
        public float Fuel
        {
            get => _fuel;
            private set
            {
                _fuel = Mathf.Clamp01(value);
                if(_fuel <= 0)
                    OnDeplete?.Invoke();
            }
        }


        private GroundCheck _groundCheck;
        private bool depleting;

        private void Awake()
        {
            _groundCheck = GetComponentInChildren<GroundCheck>();
        }

        private void Update()
        {
            if (_groundCheck.IsGrounded)
                Fuel += Time.deltaTime * (1 / groundedRechargeRate);

            else if (depleting)
                Fuel -= Time.deltaTime * (1 / depletionRate);
        }

        public void EnableDepletion() => depleting = true;
        public void DisableDepletion() => depleting = false;
        public void DepletePip() => Fuel -= Fuel % (1f / pips);
    }
}
