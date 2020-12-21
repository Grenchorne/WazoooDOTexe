using System;
using Adhaesii.WazoooDOTexe.Old;
using Adhaesii.WazoooDOTexe.Pooling;
using Sirenix.OdinInspector;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Adhaesii.WazoooDOTexe
{
    public class Breakable : SerializedMonoBehaviour, IReceiveDamage
    {
        private enum BreakAction
        {
            DoNothing,
            Disable,
            Destroy
        }

        [SerializeField]
        private BreakAction breakAction = BreakAction.Disable;
        
        [SerializeField] private GameObject breakFX;

        [SerializeField]
        private bool useHealth;

        private void Awake()
        {
            if (!useHealth || !TryGetComponent(out HealthController healthController)) return;
            healthController.OnDie += DoBreak;
        }

        public void Damage(GameObject source)
        {
            if (!useHealth) DoBreak();
        }

        private void DoBreak()
        {
            if (breakFX)
            {
                GameObject breakInstance = Instantiate(breakFX);
                breakInstance.transform.position = transform.position;

                if (breakInstance.TryGetComponent(out ISpawner spawner))
                    spawner.Spawn(transform.position);
                
                Destroy(breakInstance, 2f);
            }

            switch (breakAction)
            {
                case BreakAction.DoNothing:
                    break;
                case BreakAction.Disable:
                    gameObject.SetActive(false);
                    break;
                case BreakAction.Destroy:
                    Destroy(gameObject);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();

            }
        }
    }
}