using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;

namespace Adhaesii.WazoooDOTexe.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : SerializedMonoBehaviour
    {
        [SerializeField]
        private AudioClip gameplayMusic;

        [SerializeField]
        private AudioClip bossMusic;

        [SerializeField]
        private AudioMixer audioMixer;

        [SerializeField]
        private AudioMixerSnapshot gameplaySnapshot;
        
        [SerializeField]
        private AudioMixerSnapshot menuSnapshot;
        
        private AudioSource AudioSource { get; set; }

        private void Awake() => AudioSource = GetComponent<AudioSource>();

        public void PlayGameplay() => PlayMusic(gameplayMusic);
        public void PlayBoss() => PlayMusic(bossMusic);

        private void PlayMusic(AudioClip clip)
        {
            AudioSource.clip = clip;
            AudioSource.Play();
        }

        [Button]
        public void ChangeToMenu()
        {
            StartCoroutine(_());

            IEnumerator _()
            {
                audioMixer.TransitionToSnapshots(new[] {menuSnapshot}, new []{1f}, 1f);
                yield break;
            }
        }

        [Button]
        public  void ChangeToGameplay()
        {
            StartCoroutine(_());

            IEnumerator _()
            {
                audioMixer.TransitionToSnapshots(new[] {gameplaySnapshot}, new []{1f}, 1f);
                yield break;
            }
        }
        
    }
}
