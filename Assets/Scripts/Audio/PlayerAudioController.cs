using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class PlayerAudioController : SerializedMonoBehaviour
    {
        [TitleGroup("One-Shot Audio")]
        [SerializeField]
        private AudioClip jump;

        //[TitleGroup("One-Shot Audio")]
        [SerializeField]
        private AudioClip attack;

        [TitleGroup("Hover Audio")]
        [SerializeField]
        private AudioClip hoverLoop;
        
        [SerializeField]
        private float hoverRampTime = 0.33f;
        
        [SerializeField]
        private AudioSource hoverAudioSource;

        [TitleGroup("Footsteps Audio")]
        [SerializeField]
        private AudioClip[] footsteps;

        [SerializeField]
        private float footstepsInterval = 0.33f;

        [SerializeField]
        private float minPitch = 0.9f;

        [SerializeField]
        private float maxPitch = 1.1f;

        [SerializeField]
        private AudioSource footstepsAudioSourcePrefab;

        private bool  footstepsLooping, hoverLooping;
        
        private AudioSource AudioSource { get; set; }

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            StartCoroutine(HoverRoutine());
            StartCoroutine(WalkRoutine());
        }

        private void OnEnable()
        {
            hoverAudioSource.volume = 0f;
            hoverAudioSource.Play();
        }

        public void PlayJump()
        {
            if(!jump)
                return;
            AudioSource.PlayRandom(new []{jump}, 0.9f, 1.1f);
            AudioSource.Play();
        }

        public void PlayAttack()
        {
            if(!attack)
                return;
            AudioSource.PlayRandom(new []{attack}, 0.9f, 1.1f);
            AudioSource.Play();
        }

        private IEnumerator HoverRoutine()
        {
            bool wantsToRampUp = false;
            bool wantsToRampDown = true;
            float volumeTarget;

            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            do
            {
//                if (hoverLooping)
//                {
//                    // lets ramp down
//                    if (wantsToRampDown)
//                    {
//                        volumeTarget = 0f;
//                        wantsToRampDown = false;
//                    }
//                    
//                    // lets ramp up
//                    else if (wantsToRampUp)
//                    {
//                        volumeTarget = 1;
//                        wantsToRampUp = false;
//                    }
//                }
//                
                
                yield return wait;
            } while (true);
            
            
            hoverAudioSource.volume = 1f;

            
        }

        
        private IEnumerator WalkRoutine()
        {
            bool wantsToStart, wantsToStop;
            float volumeTarget = 0;
            
            WaitForEndOfFrame wait = new WaitForEndOfFrame();
            do
            {
                if (hoverAudioSource.volume < volumeTarget)
                {
                    
                }
                
                hoverAudioSource.volume += Mathf.Clamp01(Time.deltaTime / hoverRampTime);
                yield return wait;
            } while (hoverAudioSource.volume <= 1);
            
            yield return null;
        }

        public void StartHover()
        {
            StopAllCoroutines();
            
            hoverAudioSource.volume = 0f;

            // Fade in
            StartCoroutine(_());

            IEnumerator _()
            {
                WaitForEndOfFrame wait = new WaitForEndOfFrame();
                do
                {
                    hoverAudioSource.volume += Mathf.Clamp01(Time.deltaTime / hoverRampTime);
                    yield return wait;
                } while (hoverAudioSource.volume <= 1);
            }
        }

        private void StopHover()
        {
            hoverLooping = false;
            return;
            StopAllCoroutines();
            
            hoverAudioSource.volume = 1f;

            // Fade out
            StartCoroutine(_());

            IEnumerator _()
            {
                WaitForEndOfFrame wait = new WaitForEndOfFrame();
                do
                {
                    hoverAudioSource.volume -= Mathf.Clamp01(Time.deltaTime / hoverRampTime);
                    yield return wait;
                } while (hoverAudioSource.volume >= 1);
            }

        }

        public void StartFootsteps()
        {
            footstepsLooping = true;
            
            // Loop footsteps
            IEnumerator _()
            {
                WaitForSeconds wait = new WaitForSeconds(footstepsInterval); 
                do
                {
                    AudioSource footstepSource = Instantiate(footstepsAudioSourcePrefab).GetComponent<AudioSource>();
                    footstepSource.PlayRandom(footsteps, minPitch, maxPitch);
                    Destroy(footstepSource.gameObject, 2);
                    
                    yield return wait;
                } while (footstepsLooping);
            }
        }

        private void StopFootsteps() => footstepsLooping = false;
    }
}
