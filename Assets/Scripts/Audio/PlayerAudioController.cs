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

        [TitleGroup("Health Audio")]
        [SerializeField]
        private AudioClip[] hitClips;
        
        [SerializeField]
        private AudioClip[] deathClips;
        
        private AudioSource AudioSource { get; set; }

        private void Awake() => AudioSource = GetComponent<AudioSource>();

        private void Start()
        {
            hoverAudioSource.volume = 0f;
            hoverAudioSource.Play();
        }

        public void PlayJump() => PlayOneShot(new []{jump}, jump != null);

        public void PlayAttack() => PlayOneShot(new []{attack}, attack != null);

        public void PlayHit() => PlayOneShot(hitClips, hitClips != null);

        public void PlayDie() => PlayOneShot(deathClips, deathClips != null);
        
        


        public void ProcessHover(bool isHovering)
        {
            float sourceVolume = hoverAudioSource.volume;
            hoverAudioSource.volume =
                Mathf.Clamp01(sourceVolume + (Time.deltaTime / hoverRampTime * (isHovering ? 1 : -1)));
        }

        // "YOINK!" -- WazoooVoice.cs
        private void PlayOneShot(AudioClip[] clips, bool playCondition = true, float minPitch = 0.9f, float maxPitch = 1.1f)
        {
            if(!playCondition)
                return;
            AudioSource.PlayRandom(clips, minPitch, maxPitch);
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
