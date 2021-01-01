using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class KillTracker : SerializedMonoBehaviour
    {
        public int Kills { get; private set; }
        
        private void Awake()
        {

            var slimes = FindObjectsOfType<EnemyPatroller>();
            foreach (EnemyPatroller slime in slimes)
                slime.GetComponent<HealthController>().OnDie += () => Kills++;

            FindObjectOfType<Slimeboss>(true).GetComponent<HealthController>().OnDie += () => Kills++;
        }
    }
}
