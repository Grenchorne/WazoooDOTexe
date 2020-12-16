using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    [CreateAssetMenu(fileName = "Random Sprites", menuName = "WDX/Create Random Sprite Selection")]
    public class RandomSpriteSelection : SerializedScriptableObject
    {
        [SerializeField]
        private Sprite[] sprites;

        public Sprite GetRandom() => sprites[Random.Range(0, sprites.Length - 1)];
    }
}
