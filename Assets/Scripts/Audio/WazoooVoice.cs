using System;
using System.Collections.Generic;
using System.Linq;
using Adhaesii.WazoooDOTexe.Player;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Audio;
using Random = UnityEngine.Random;


namespace Adhaesii.WazoooDOTexe.Audio
{
    
    [RequireComponent(typeof(AudioSource))]
    public class WazoooVoice : SerializedMonoBehaviour
    {
        [SerializeField]
        private bool disallowInterrupts = true;
        
        [SerializeField]
        private ClipCollection hurtClips;

        [SerializeField]
        private ClipCollection despawnClips;

        [SerializeField]
        private ClipCollection deathClips;

        [SerializeField]
        private ClipCollection seeEnemyClips;

        [SerializeField]
        private ClipCollection killEnemyClips;

        [SerializeField]
        private ClipCollection seeBossClips;
        
        [SerializeField]
        private ClipCollection killBossClips;

        [SerializeField]
        private bool genericUnlockables = false;

        [SerializeField, ShowIf("genericUnlockables")]
        private ClipCollection unlockablesClips;

        [SerializeField, HideIf("genericUnlockables")]
        private ClipCollection unlockHoverClips;
        
        [SerializeField, HideIf("genericUnlockables")]
        private ClipCollection unlockMeleeCLips;

        [SerializeField, HideIf("genericUnlockables")]
        private ClipCollection unlockHoverJumpClips;
        
        [SerializeField, HideIf("genericUnlockables")]
        private ClipCollection unlockRangedClips;

        [SerializeField]
        private ClipCollection openDoorClips;
        
        private AudioSource AudioSource { get; set; }

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        private void PlayClipFromCollection(ClipCollection clipCollection)
        {
            if (Random.Range(0, 1) > clipCollection.Probability || disallowInterrupts && AudioSource.isPlaying)
                return;

            AudioClip audioClip =
                clipCollection.GetAudioClip(out float pitch, out float volume, out AudioMixerGroup audioMixerGroup);

            AudioSource.clip = audioClip;
            AudioSource.volume = volume;
            AudioSource.pitch = pitch;
            if(audioMixerGroup)
                AudioSource.outputAudioMixerGroup = audioMixerGroup;      
            AudioSource.Play();
        }
        
        [Button, HorizontalGroup("Health")]
        public void PlayHurt() => PlayClipFromCollection(hurtClips);

        [Button, HorizontalGroup("Health")]
        public void PlayDespawn() => PlayClipFromCollection(despawnClips);
        
        [Button, HorizontalGroup("Health")]
        public void PlayDeath() => PlayClipFromCollection(deathClips);
        
        [Button, HorizontalGroup("Enemies")]
        public void PlaySeeEnemy() => PlayClipFromCollection(seeEnemyClips);
        
        [Button, HorizontalGroup("Enemies")]
        public void PlayKillEnemy() => PlayClipFromCollection(killEnemyClips);
        
        [Button, HorizontalGroup("Boss")]
        public void PlaySeeBoss() => PlayClipFromCollection(seeBossClips);
        
        [Button, HorizontalGroup("Boss")]
        public void PlayKillBoss() => PlayClipFromCollection(killBossClips);
        
        [Button, HorizontalGroup("Interaction")]
        public void PlayOpenDoor() => PlayClipFromCollection(openDoorClips);

        [Button]
        public void PlayUnlock(PlayerAbilityUnlocker.Ability playerAbility, bool useGeneric = false)
        {
            if (useGeneric || playerAbility == default)
            {
                PlayClipFromCollection(unlockablesClips);
                return;
            }

            switch (playerAbility)
            {
                case PlayerAbilityUnlocker.Ability.Jump:
                    break;
                case PlayerAbilityUnlocker.Ability.Attack:
                    PlayClipFromCollection(unlockMeleeCLips);
                    break;
                case PlayerAbilityUnlocker.Ability.Hover:
                    PlayClipFromCollection(unlockHoverClips);
                    break;
                case PlayerAbilityUnlocker.Ability.HoverJump:
                    PlayClipFromCollection(unlockHoverJumpClips);
                    break;
                case PlayerAbilityUnlocker.Ability.Ranged:
                    PlayClipFromCollection(unlockRangedClips);
                    break;
            }
            
        }

        [Serializable]
        private class ClipCollection
        {
            [SerializeField]
            private WeightedClip.Container clipContainer;

            [ShowInInspector]
            private AudioClip IngestClip
            {
                get => null;
                set => clipContainer.Ingest(value);
            }

            public AudioClip GetAudioClip(out float pitch, out float volume, out AudioMixerGroup mixerGroup)
            {
                pitch = Random.Range(minPitch, maxPitch);
                volume = this.volume;
                mixerGroup = this.mixerGroup;

                return clipContainer.GetRandom();
            }
            
            [SerializeField]
            private float minPitch = 0.9f;
            [SerializeField]
            private float maxPitch = 1.2f;

            [SerializeField]
            private float volume = 1;

            [SerializeField]
            private AudioMixerGroup mixerGroup;

            [Range(0, 1), Tooltip("Probability of a clip firing on the event invocation")]
            [SerializeField]
            private float _probability = 1f;
            public float Probability => _probability;

            [Serializable]
            private class WeightedClip
            {
                [HorizontalGroup("Clip")]
                public AudioClip clip;
                [HorizontalGroup("Clip"), Range(0, 1)]
                public float weight;

                public WeightedClip(AudioClip clip, float weight = 1)
                {
                    this.clip = clip;
                    this.weight = weight;
                }

                [Serializable]
                public class Container
                {
                    [SerializeField]
                    private WeightedClip[] clips;

                    public void Ingest(AudioClip clip)
                    {
                        List<WeightedClip> clipsList;
                        if (clips == null || clips.Length < 1)
                        {
                            clipsList = new List<WeightedClip> {new WeightedClip(clip)};
                        }
                        else
                        {
                            clipsList = clips.ToList();
                            clipsList.Add(new WeightedClip(clip));
                            
                        }
                        clips = clipsList.ToArray();
                    }

                    public AudioClip GetRandom()
                    {
                        float[] weights = new float[clips.Length];
                        
                        // Get a random value assigned to weight of each clip
                        for (int i = 0; i < clips.Length; i++)
                            weights[i] = Random.Range(0, clips[i].weight);

                        float highestRoll = 0;
                        int highestIndex = 0;

                        // check to see which is highest
                        for (int i = 0; i < weights.Length; i++)
                        {
                            if (weights[i] > highestRoll)
                            {
                                highestRoll = weights[i];
                                highestIndex = i;
                            }
                        }
                        return clips[highestIndex].clip;
                    }


                }
            }
        }
    }
}
