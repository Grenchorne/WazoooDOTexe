using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    [RequireComponent(typeof(Animator))]
    public class StateDrivenCameraController : SerializedMonoBehaviour
    {
        private Animator Animator { get; set; }

        [SerializeField]
        private string gameplayState = "Gameplay";

        [SerializeField]
        private string bossBattleState = "BossBattle";

        private void Awake()
        {
            Animator = GetComponent<Animator>();
        }

        [Button]
        public void ChangeToGameplay() => Animator.Play(gameplayState);

        [Button]
        public void ChangeToBossFight() => Animator.Play(bossBattleState);
    }
}
