using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class RandomClipPlayer : SerializedMonoBehaviour
    {
        [SerializeField]
        private AudioClip[] clips;

        [SerializeField]
        private bool playOnEnable = true;

        [SerializeField]
        private float minPitch = 0.9f;

        [SerializeField]
        private float maxPitch = 1.1f;        

        private void OnEnable()
        {
            if (playOnEnable)
                Play();
        }

        public void Play(AudioSource audioSource = null)
        {
            if (audioSource == null)
                audioSource = GetComponent<AudioSource>();
            
            audioSource.PlayRandom(clips, minPitch, maxPitch);
        }
        
    }

    public static class Audio
    {
        public static void PlayRandom(this AudioSource audioSource, AudioClip[] clips, float minPitch = 1,
            float maxPitch = 1)
        {
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            
            audioSource.clip = clips[Random.Range(0, clips.Length - 1)];
            audioSource.Play();
        }
    }
}
