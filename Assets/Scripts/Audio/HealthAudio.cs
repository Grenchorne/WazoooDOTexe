using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class HealthAudio : SerializedMonoBehaviour
    {
        [SerializeField, Required]
        private AudioClip[] hitClips;

        [SerializeField, Required]
        private AudioClip[] deathClips;

        [SerializeField, Required]
        private AudioSource audioSourcePrefab;
        
        private AudioSource AudioSource { get; set; }

        private void Awake() => AudioSource = GetComponent<AudioSource>();

        public void PlayHit() => AudioSource.PlayRandom(hitClips, 0.9f, 1.1f);

        public void PlayDie()
        {
            Audio.PlayRandomSpawn(audioSourcePrefab, 3f, deathClips, 0.9f, 1.1f);
        }
    }
}
