using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Pooling
{
    [RequireComponent(typeof(IPoolable))]
    public class SpawnOnPool : SerializedMonoBehaviour
    {
        // Yes I realize the irony of instantiating and not pooling in this script
        [SerializeField, AssetsOnly]
        private GameObject prefab;

        [SerializeField]
        private float destroyDelay = 3f;

        [SerializeField]
        private bool enabled = true;

        private bool started;

        private void Awake()
        {
            IPoolable poolable = GetComponent<IPoolable>();
            poolable.DisableEvent += () => Spawn(poolable.Transform.position, destroyDelay);
        }

        private void Start()
        {
            started = true;
        }

        [Button]
        private GameObject Spawn(Vector2 position, float delay = 0)
        {
            if (!started || !enabled)
                return null;
            GameObject instance = Instantiate(prefab);
            instance.transform.position = position;
            if(Mathf.Abs(delay) > 0)
                Destroy(instance, delay);
            return instance;
        }
    }
}