using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace Adhaesii.WazoooDOTexe.UI
{
    [RequireComponent(typeof(MaskSlider))]
    public class HealthBar : SerializedMonoBehaviour
    {
        [SerializeField]
        private HealthController playerHealth;

        private MaskSlider maskSlider;

        private void Awake() => maskSlider = GetComponent<MaskSlider>();

        private void OnEnable()
        {
            playerHealth.OnDamage += UpdateHealth;
            playerHealth.OnDie += UpdateHealth;
        }

        private void OnDisable()
        {
            playerHealth.OnDamage -= UpdateHealth;
            playerHealth.OnDie -= UpdateHealth;
        }

        private void UpdateHealth()
        {
            maskSlider.Value = (float) playerHealth.Health / (float) playerHealth.MaxHealth;
        }
    }
}
