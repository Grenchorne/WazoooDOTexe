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

        private SpriteRenderer[] spriteRenderers;
        private Material[] baseMats;

        private void Awake()
        {
            // Establish base mats
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
            
            baseMats = new Material[spriteRenderers.Length];
            for (int i = 0; i < baseMats.Length; i++) baseMats[i] = spriteRenderers[i].material;
        }

        [Button]
        public void ShowFX(float frequency = -1f, float duration = -1f)
        {
            if (frequency < 0) frequency = flashFrequency;
            if (duration < 0) duration = flashDuration;
            
            StopAllCoroutines();
            
            ReturnToBase();
            
            StartCoroutine(flashColour_());

            IEnumerator flashColour_()
            {
                if(!flashColor)
                    yield break;
                float startTime = Time.time;
                float time;

                Material flashMat = Resources.Load<Material>("WhiteFlash");


                // cache a WaitFor
                WaitForSeconds wait = new WaitForSeconds(frequency);

                bool baseMat = true;
                do
                {
                    time = Time.time;
                    for (int i = 0; i < spriteRenderers.Length; i++)
                        spriteRenderers[i].material = baseMat ? flashMat : baseMats[i];
                    yield return wait;
                    baseMat = !baseMat;
                } while (startTime + duration > time);

                // Reset to base mat
                ReturnToBase();
            }

        }

        private void ReturnToBase()
        {
            for (int i = 0; i < spriteRenderers.Length; i++) spriteRenderers[i].material = baseMats[i];
        }
    }
}
