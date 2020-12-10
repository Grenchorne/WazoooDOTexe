using System;
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
        
        private AudioSource AudioSource { get; set; }

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        private void Start()
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

        public void ProcessHover(bool isHovering)
        {
            float sourceVolume = hoverAudioSource.volume;
            hoverAudioSource.volume =
                Mathf.Clamp01(sourceVolume + (Time.deltaTime / hoverRampTime * (isHovering ? 1 : -1)));
        }

        private float t_footstep;
        public void ProcessFootsteps(bool playFootsteps)
        {
            if (!playFootsteps)
            {
                t_footstep = 0; // always return to 0 so that a footstep plays at the start of each walk cycle
                return;
            }

            if (t_footstep > 0)
            {
                t_footstep -= Time.deltaTime; // we can get away with this here because it's called every frame, but not advisable
                return;
            }

            AudioSource footstepSource = Instantiate(footstepsAudioSourcePrefab).GetComponent<AudioSource>();
            footstepSource.PlayRandom(footsteps, minPitch, maxPitch);
            Destroy(footstepSource.gameObject, 2);
            t_footstep = footstepsInterval;
        }
    }
}
