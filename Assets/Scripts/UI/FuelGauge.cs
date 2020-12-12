using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Adhaesii.WazoooDOTexe.UI
{
    [RequireComponent(typeof(MaskSlider))]
    public class FuelGauge : SerializedMonoBehaviour
    {
        [SerializeField] private FuelHandler _fuelHandler;

        private MaskSlider slider;

        private void Awake()
        {
            slider = GetComponent<MaskSlider>();
        }

        private void Update()
        {
            slider.Value = _fuelHandler.Fuel;
        }
    }
}
