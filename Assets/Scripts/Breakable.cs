using System;
using Adhaesii.WazoooDOTexe.Old;
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

        private bool subscribed;
        public void Damage(GameObject source)
        {
            if (!useHealth)
            {
                doBreak_();
                return;
            }
            
            if(TryGetComponent(out HealthController healthController))
            {
                if (!subscribed)
                {
                    healthController.OnDie += doBreak_;
                    subscribed = true;
                }
                healthController.Damage(gameObject);
            }

            void doBreak_()
            {
                print("BREAK");
                if (breakFX)
                {
                    GameObject breakInstance = Instantiate(breakFX);
                    breakInstance.transform.position = transform.position;
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
}