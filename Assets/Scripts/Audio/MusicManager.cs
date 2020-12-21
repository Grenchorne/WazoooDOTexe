using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : SerializedMonoBehaviour
    {
        [SerializeField]
        private AudioClip gameplayMusic;

        [SerializeField]
        private AudioClip bossMusic;
        
        private AudioSource AudioSource { get; set; }

        private void Awake() => AudioSource = GetComponent<AudioSource>();

        public void PlayGameplay() => PlayMusic(gameplayMusic);
        public void PlayBoss() => PlayMusic(bossMusic);

        private void PlayMusic(AudioClip clip)
        {
            AudioSource.clip = clip;
            AudioSource.Play();
        }
    }
}
