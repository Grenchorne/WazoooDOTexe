using System;
using System.Collections;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Adhaesii.WazoooDOTexe
{
    [RequireComponent(typeof(Collider2D), typeof(Rigidbody2D), typeof(HealthController))]
    public class Slimeboss : SerializedMonoBehaviour
    {
        [SerializeField]
        private Animator slimeBossAnimator;
        
        public UnityEvent OnDeath;

        [SerializeField]
        private GameObject announceGameObject;

        [SerializeField]
        private GameObject damage;

        [SerializeField]
        private GameObject deathFXPrefab;

        [SerializeField]
        private BossRoomData bossRoomData;

        [SerializeField]
        private Encounter encounter;

        [SerializeField, Header("Behaviour Timings")]
        private float moveSpeed = 0.6f;

        [SerializeField]
        private float appearDuration = 0.6f;

        [SerializeField]
        private float disappearDuration = 0.6f;

        [SerializeField]
        private float waitBetweenLoops = 2f;
        
        private Rigidbody2D Rigidbody2D { get; set; }
        private HealthController HealthController { get; set; }
        private SlimeBossStateController StateController { get; set; }

        private void Awake()
        {
            Rigidbody2D = GetComponent<Rigidbody2D >();
            HealthController = GetComponent<HealthController>();

            HealthController.OnDie += () => OnDeath?.Invoke();

            //SlimeBossState.Death deathState = ;

            HealthController.OnDie += () =>
            {
                StopAllCoroutines();
                gameObject.SetActive(true);
                damage.SetActive(false);
                StartCoroutine(_());
                IEnumerator _()
                {
                    StateController.ChangeState(new SlimeBossState.Death(this, slimeBossAnimator, 3f, deathFXPrefab));
                    yield return  new WaitForSeconds(6);
                    // force it to exit the death state
                    StateController.ChangeState(new SlimeBossState.NoState(this, slimeBossAnimator, 0f));
                    
                    encounter.Complete();
                    
                    Destroy(gameObject);
                }
            };
            
            StateController = new SlimeBossStateController();
        }

        private void Start()
        {
            StartCoroutine(slimeBossBehaviour_());
            IEnumerator slimeBossBehaviour_()
            {
                Vector2 appearPoint = Vector2.zero;
                Vector2 moveToPoint = Vector2.zero;
                
                // Define a standard wait (uses a predicate so we know it'll use the proper state)
                // our "machine" always just waits until the state tells us it's finished - defined per-state
                WaitUntil wait = new WaitUntil(() => StateController.CurrentState.IsFinished);
                assignPoints_();

                while (true)
                {
                    announceGameObject.SetActive(false);
                    
                    // create and execute an appear state
                    SlimeBossState.Appear appear =
                        new SlimeBossState.Appear(this, slimeBossAnimator, appearPoint, appearDuration);
                    
                    damage.SetActive(true); // enable the damage here
                    StateController.ChangeState(appear);
                    yield return wait; 

                    // create and execute a move state
                    SlimeBossState.MoveTo moveTo = new SlimeBossState.MoveTo(this, slimeBossAnimator, 3f,
                        moveToPoint, moveSpeed);
                    StateController.ChangeState(moveTo);
                    yield return wait;
                    
                    // create and execute a disappear state
                    SlimeBossState.Disappear disappear = new SlimeBossState.Disappear(this, slimeBossAnimator, disappearDuration);
                    StateController.ChangeState(disappear);
                    damage.SetActive(false); // disable the damage here
                    yield return wait;
                    
                    // create and execute a "no" state -- this is used to extend the "hidden" duration
                    SlimeBossState.NoState noState = new SlimeBossState.NoState(this, slimeBossAnimator, waitBetweenLoops/2);
                    StateController.ChangeState(noState);
                    yield return wait;
                    
                    // Find a random point and tell the player where the boss will spawn
                    assignPoints_();
                    announceGameObject.transform.position = appearPoint;
                    announceGameObject.SetActive(true);
                    
                    yield return  new WaitForSeconds(waitBetweenLoops/2);

                }

                void assignPoints_()
                {
                    // With this section, we basically just pick between two of the end points
                    int i = Random.Range(0, 1);
                    switch (i)
                    {
                        case 0:
                            appearPoint = bossRoomData.appearPoints[0].position;
                            moveToPoint = bossRoomData.appearPoints[1].position;
                            break;
                        case 1:
                            appearPoint = bossRoomData.appearPoints[1].position;
                            moveToPoint = bossRoomData.appearPoints[0].position;
                            break;
                    }
                }
            }
        }

        private void Update() => StateController.Tick(Time.deltaTime);

        [Serializable]
        private class BossRoomData
        {
            public Transform[] appearPoints;
            public Vector2 GetRandomAppearPoint() => appearPoints[Random.Range(0, appearPoints.Length)].position;
            public Transform[] moveToPoints;
            public Vector2 GetRandomMoveToPoint() => moveToPoints[Random.Range(0, moveToPoints.Length)].position;
        }

        private class SlimeBossStateController
        {
            private SlimeBossState _currentState;
            public SlimeBossState CurrentState => _currentState;

            public void ChangeState<T>(T newState) where T : SlimeBossState 
            {
                if(newState == _currentState)
                    return;

                _currentState?.Exit();
                _currentState = newState;
                _currentState.Enter();
            }

            public void Tick(float deltaTime) => _currentState?.Tick(deltaTime);
        }

        private abstract class SlimeBossState
        {
            private static class AnimationParameters
            {
                public static string Idle = "Idle";
                public static string Jump = "Jump";
                public static string Sleep = "Sleep";
                public static string Spin = "Spin";
                public static string Walk = "Walk";
            }
            
            public bool IsFinished { get; protected set; }
            public float Lifetime { get; private set; }
            
            protected Slimeboss slimeboss;
            protected Rigidbody2D rigidbody2D;
            protected readonly Animator animator;
            protected readonly float duration;

            protected SlimeBossState(Slimeboss slimeboss, Animator animator, float duration)
            {
                this.slimeboss = slimeboss;
                this.rigidbody2D = this.slimeboss.Rigidbody2D;
                this.animator = animator;
                this.duration = duration;
            }

            public abstract void Enter();

            public virtual void Tick(float deltaTime) => Lifetime += deltaTime;

            public abstract void Exit();

            public abstract class TimedState : SlimeBossState
            {
                protected TimedState(Slimeboss slimeboss, Animator animator, float duration) : base(slimeboss, animator, duration)
                {
                }

                public override void Tick(float deltaTime)
                {
                    base.Tick(deltaTime);
                    if (duration < Lifetime)
                        IsFinished = true;
                }
            }

            public class Appear : TimedState
            {
                private readonly Vector3 targetPos;
                private readonly float duration;
                
                public Appear(Slimeboss slimeboss, Animator animator, Vector3 targetPos, float duration) : base(slimeboss, animator, duration)
                {
                    this.targetPos = targetPos;
                }

                public override void Enter()
                {
                    slimeboss.transform.position = targetPos;
                    animator.enabled = true;
                    animator.GetComponent<SpriteRenderer>().enabled = true;
                    animator.SetTrigger(AnimationParameters.Spin);
                }

                public override void Exit()
                {
                    
                }
            }

            public class Disappear : TimedState
            {   
                public Disappear(Slimeboss slimeboss, Animator animator, float duration) : base(slimeboss, animator, duration)
                {
                }
              
                public override void Enter()
                {
                    animator.SetTrigger(AnimationParameters.Spin);
                }

                public override void Exit()
                {
                    animator.enabled = false;
                    animator.GetComponent<SpriteRenderer>().enabled = false;
                }
            }

            public class NoState : TimedState
            {
                public NoState(Slimeboss slimeboss, Animator animator, float duration) : base(slimeboss, animator, duration)
                {
                }

                public override void Enter()
                {
                    
                }

                public override void Exit()
                {
                    
                }
            }

            public class MoveTo : SlimeBossState
            {
                private Vector2 target;
                private float speed;
                
                public MoveTo(Slimeboss slimeboss, Animator animator, float duration, Vector2 target, float speed) : base(slimeboss, animator, duration)
                {
                    this.target = target;
                    this.speed = speed;
                }

                public override void Enter()
                {
                    animator.GetComponent<SpriteRenderer>().flipX = target.x < slimeboss.transform.position.x;
                    animator.SetTrigger(AnimationParameters.Walk);
                }

                public override void Tick(float deltaTime)
                {
                    base.Tick(deltaTime);
                    slimeboss.transform.position = Vector3.MoveTowards(slimeboss.transform.position, target, speed);
                    if (Vector3.Distance(slimeboss.transform.position, target) < 0.5f)
                        IsFinished = true;
                }

                public override void Exit()
                {
                    
                }   
            }

            public class Death : SlimeBossState
            {
                private readonly GameObject deathPrefab;
                private GameObject deathFX;
                
                public Death(Slimeboss slimeboss, Animator animator, float duration, GameObject deathPrefab) : base(slimeboss, animator, duration)
                {
                    this.deathPrefab = deathPrefab;
                }

                public override void Enter()
                {
                    animator.enabled = false;
                    animator.GetComponent<SpriteRenderer>().enabled = false;
                    deathFX = Instantiate(deathPrefab);
                    deathFX.transform.position = slimeboss.transform.position;
                }

                public override void Exit()
                {
                    Destroy(deathFX);
                }
                
            }
        }
    }
}
