using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class HitSpriteFX : SerializedMonoBehaviour
    {
        [SerializeField]
        private bool flashColor = true;

        [SerializeField]
        private float flashFrequency = 0.3f;

        [SerializeField]
        private float flashDuration = 1f;

        [Button]
        public void ShowFX(float frequency = -1f, float duration = -1f)
        {
            if (frequency < 0) frequency = flashFrequency;
            if (duration < 0) duration = flashDuration;

            StartCoroutine(flashColour_());

            IEnumerator flashColour_()
            {
                if(!flashColor)
                    yield break;
                float startTime = Time.time;
                float time;
                var srs = GetComponentsInChildren<SpriteRenderer>();

                Material flashMat = Resources.Load<Material>("WhiteFlash");

                // Establish base mats
                Material[] baseMats = new Material[srs.Length];

                for (int i = 0; i < baseMats.Length; i++) baseMats[i] = srs[i].material;

                // cache a WaitFor
                WaitForSeconds wait = new WaitForSeconds(frequency);

                bool baseMat = true;
                do
                {
                    time = Time.time;
                    for (int i = 0; i < srs.Length; i++)
                        srs[i].material = baseMat ? flashMat : baseMats[i];
                    yield return wait;
                    baseMat = !baseMat;
                } while (startTime + duration > time);

                // Reset to base mat
                for (int i = 0; i < srs.Length; i++) srs[i].material = baseMats[i];
            }
        }
    }
}
