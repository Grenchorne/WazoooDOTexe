using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Adhaesii.WazoooDOTexe.AI;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Player
{
    public class EnemySensor : Singleton<EnemySensor>
    {
        public float tickRate = 0.5f;
        private float t;
        
        public event Action<GameObject> OnEnemyDie;
        public event Action<GameObject> OnSeeNewEnemy;
        public event Action<GameObject> OnBossDie;
        public event Action<GameObject> OnSeeBoss;
        
        private List<EnemyPatroller> enemyPatrollers;
        private List<EnemySensorTracker> enemySensorTrackers;

        private Slimeboss slimeboss;
        private EnemySensorTracker bossSensorTracker;
        
        private void Awake()
        {
            enemyPatrollers = FindObjectsOfType<EnemyPatroller>().ToList();
            enemySensorTrackers = new List<EnemySensorTracker>();

            slimeboss = FindObjectOfType<Slimeboss>(true);
            slimeboss.TryGetComponent(out bossSensorTracker);
        }

        private void Start()
        {
            // Let's wait a second before executing this so we know that everything is preparred
            StartCoroutine(_());
            IEnumerator _()
            {
                WaitForEndOfFrame wait = new WaitForEndOfFrame();
                yield return new WaitForSeconds(1);
                
                foreach (EnemyPatroller patroller in enemyPatrollers)
                {
                    if (patroller.TryGetComponent(out HealthController healthController))
                        healthController.OnDie += () => EnemyHasDied(patroller.gameObject);
                    if (patroller.TryGetComponent(out EnemySensorTracker sensorTracker))
                    {
                        sensorTracker.OnSee += () => SeeEnemy(patroller.gameObject);
                        enemySensorTrackers.Add(sensorTracker);
                    }
                    yield return wait;
                }

                slimeboss.GetComponent<HealthController>().OnDie += () => BossHasDied(slimeboss.gameObject);
                bossSensorTracker.OnSee += () => SeeBoss(slimeboss.gameObject);
            }
        }

        public void EnemyHasDied(GameObject enemyGameObject)
        {
            OnEnemyDie?.Invoke(enemyGameObject);
//            Debug.Log(gameObject.name + " died");
        }

        public void SeeEnemy(GameObject gameObject)
        {
            OnSeeNewEnemy?.Invoke(gameObject);
//            Debug.Log(gameObject.name + " seen");
        }

        public void SeeBoss(GameObject bossObject)
        {
            OnSeeBoss?.Invoke(bossObject);
//            Debug.Log(gameObject.name + " seen");
        }

        public void BossHasDied(GameObject bossObject)
        {
            OnBossDie?.Invoke(bossObject);
//            Debug.Log(gameObject.name + " died");
        }

        private void Update()
        {
            t += Time.deltaTime;
            if (t < tickRate)
                return;
            t = 0;

            foreach (EnemyPatroller enemyPatroller in enemyPatrollers)
            {
                if (enemyPatroller.TryGetComponent(out EnemySensorTracker sensorTracker) &&
                    enemyPatroller.GetComponentInChildren<SpriteRenderer>().isVisible)
                {
                    sensorTracker.See();
                }
            }
        }
    }
}