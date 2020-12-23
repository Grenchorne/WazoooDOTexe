using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Pooling
{
    public class CoinPoolableSpawner : SerializedMonoBehaviour, ISpawner
    {
        [SerializeField]
        private bool spawnOnAwake = false;

        [SerializeField]
        private bool spawnOnEnable = true;

        [SerializeField]
        private int amountToSpawn = 3;

        [SerializeField]
        private Vector2 directionMin = Vector2.one * -1;

        [SerializeField]
        private Vector2 directionMax = Vector2.one;

        [SerializeField]
        private float force = 5f;

        [SerializeField]
        private ForceMode2D forceMode = ForceMode2D.Impulse;

        private void Awake()
        {
            if (spawnOnAwake)
                Spawn(transform.position);
        }

        private void OnEnable()
        {
            if(spawnOnEnable)
                Spawn(transform.position);
        }

        [Button]
        public void Spawn(Vector3 position)
        {
            CoinPool pool = CoinPool.Instance;
            
            for (int i = 0; i < amountToSpawn; i++)
            {
                CoinPoolable coin = pool.GetCoin();

                coin.Transform.position = position;
                
                coin.GameObject.SetActive(true);
//                print($"Spawn: {coin.name}");

                Vector2 force = new Vector2(Random.Range(directionMin.x, directionMax.x) * this.force,
                    Random.Range(directionMin.y, directionMax.y) * this.force);
                
                coin.GetComponent<Rigidbody2D>().AddForce(force, forceMode);
            }
        }
    }

    public interface ISpawner
    {
        void Spawn(Vector3 position);
    }
}
