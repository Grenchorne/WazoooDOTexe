using Sirenix.OdinInspector;
using UnityEngine;

using UnityEngine.UI;

namespace Adhaesii.WazoooDOTexe.UI
{
    [RequireComponent(typeof(Slider))]
    public class FuelGauge : SerializedMonoBehaviour
    {
        [SerializeField] private FuelHandler _fuelHandler;

        private Slider slider;

        private void Awake()
        {
            slider = GetComponent<Slider>();
        }

        private void Update()
        {
            slider.value = _fuelHandler.Fuel;
        }
    }
}
