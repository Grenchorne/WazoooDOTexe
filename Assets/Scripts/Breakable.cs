using System;
using Adhaesii.WazoooDOTexe.Old;
using Sirenix.OdinInspector;
using UnityEngine;

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
        public void Damage(GameObject source)
        {
            if (!useHealth)
            {
                doBreak_();
                return;
            }
            
            if(TryGetComponent(out HealthController healthController) && healthController.Health == 0)
                doBreak_();

            void doBreak_()
            {
                if (breakFX)
                {
                    GameObject breakInstance = Instantiate(breakFX);
                    breakInstance.transform.position = transform.position;
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