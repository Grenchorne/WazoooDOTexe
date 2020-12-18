using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Player
{
    public class PlayerAnimation : SerializedMonoBehaviour
    {
        [SerializeField]
        private Animator animator;
        
        [SerializeField]
        private HashSet hashSet;

        private PlayerMover playerMover;
        private PlayerMeleeController playerMeleeController;
        
        private void Awake()
        {
            hashSet.Initialize();
            playerMover = GetComponent<PlayerMover>();
            playerMeleeController = GetComponentInChildren<PlayerMeleeController>();
        }

        private void Start()
        {
            playerMeleeController.OnSwing += () => animator.SetTrigger(hashSet.Melee);
            //playerMover.JumpProcessor.OnJump += SetJump;
        }

        private void Update()
        {
            //animator.SetBool(hashSet.Idle, false);
            //animator.SetTrigger(hashSet.Jump);
            //animator.SetBool(hashSet.Move, false);
        }

        private void LateUpdate()
        {
            animator.SetBool(hashSet.Move, playerMover.IsMoving);
            //animator.SetBool(hashSet.Idle, playerMover.IsIdling);
        }


        private void SetWalk(bool isWalking)
        {
            if(!isWalking)
                return;
            
            animator.SetBool(hashSet.Move, true);
        }

        private void SetJump()
        {
            animator.SetTrigger(hashSet.Jump);
        }

        private void SetIdle()
        {
            animator.SetBool(hashSet.Idle, true);
        }

        [Serializable]
        private class HashSet
        {
            [SerializeField]
            private string h_move = "IsMoving";
            public int Move { get; private set;}

            [SerializeField]
            private string h_jump = "Jump";
            public int Jump { get; private set; }

            [SerializeField]
            private string h_idle = "IsIdling";
            public int Idle { get; private set;}
            
            
            [SerializeField]
            private string h_melee = "Melee";
            public int Melee { get; private set;}

            public void Initialize()
            {
                Move = Animator.StringToHash(h_move);
                Jump = Animator.StringToHash(h_jump);
                Idle = Animator.StringToHash(h_idle);
                Melee = Animator.StringToHash(h_melee);
            }
        }
    }
}
