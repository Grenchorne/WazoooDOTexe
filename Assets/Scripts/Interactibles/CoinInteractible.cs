using Adhaesii.WazoooDOTexe.Pooling;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Interactibles
{
    [RequireComponent(typeof(CoinPoolable), typeof(Collider2D), typeof(Rigidbody2D))]
    public class CoinInteractible : SerializedMonoBehaviour
    {
        [SerializeField]
        private string playerTag = "Player";

        [SerializeField]
        private float timeToSleep = 3f;

        [SerializeField]
        private float timeToDisable;
        
        private float t_sinceAwake;
        
        private PlayerCurrency playerCurrency;

        private Rigidbody2D RigidBody2D { get; set; }

        private void Awake() => RigidBody2D = GetComponent<Rigidbody2D>();

        private void Start() => playerCurrency = FindObjectOfType<PlayerCurrency>();

        private void OnEnable()
        {
            t_sinceAwake = 0;
            RigidBody2D.isKinematic = false;
        }

        private void Update()
        {
            t_sinceAwake += Time.deltaTime;
            
            // remove it if its been hanging around too long
            if (t_sinceAwake >= timeToDisable) gameObject.SetActive(false);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag(playerTag))
            {
                // change the rb to static if it's been awake past its bedtime 
                if (!(t_sinceAwake >= timeToSleep)) return;
                RigidBody2D.isKinematic = true;
                RigidBody2D.velocity = Vector2.zero;
            }
            else
            {
                playerCurrency.Add(1);
                gameObject.SetActive(false);
            }
        }
    }
}