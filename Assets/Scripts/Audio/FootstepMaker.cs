using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Audio
{
    public class FootstepMaker : SerializedMonoBehaviour
    {
        [SerializeField]
        private AudioClip[] footsteps;
        
        [SerializeField]
        private string groundTag;

        [SerializeField]
        private GameObject footstepsAudioSourcePrefab;

        [SerializeField]
        private float minPitch = 0.9f;

        [SerializeField]
        private float maxPitch = 1.1f;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if(!other.CompareTag(groundTag))
                return;
            
            AudioSource footstepSource = Instantiate(footstepsAudioSourcePrefab).GetComponent<AudioSource>();
            footstepSource.PlayRandom(footsteps, minPitch, maxPitch);
            Destroy(footstepSource.gameObject, 2);
        }
    }
}
