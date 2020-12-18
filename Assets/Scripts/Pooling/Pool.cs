using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe.Pooling
{
    public class Pool : SerializedMonoBehaviour
    {
        [SerializeField]
        private IPoolable prefab;

        [SerializeField]
        private int count;

        [SerializeField]
        private float pollFrequency = 1f;
        private float t_poll;

        private List<IPoolable> gameObjects;
        private Queue<IPoolable> queue;
        
        public IPoolable Get() => queue.Dequeue();
        
        private void Awake()
        {
            gameObjects = new List<IPoolable>();
            queue = new Queue<IPoolable>();
            
            for (int i = 0; i < count; i++)
            {
                var instance = Instantiate(prefab.GameObject).GetComponent<IPoolable>();
                instance.GameObject.SetActive(false);
                
                instance.DisableEvent += () => queue.Enqueue(instance);
                
                gameObjects.Add(instance);
                queue.Enqueue(instance);
                instance.GameObject.SetActive(false); // this will not call DisableEvent 
            }
        }

        private void Update()
        {
            t_poll -= Time.deltaTime;
            if(t_poll > 0)
                return;
            t_poll = pollFrequency;
            
             // make inactive objects children
            Transform thisTransform = transform;
            foreach (IPoolable poolable in gameObjects)
            {
                if(!poolable.GameObject.activeSelf)
                    poolable.Transform.SetParent(thisTransform);
            }
        }
    }

    public interface IPoolable
    {
        GameObject GameObject { get; }
        Transform Transform { get; }
        event Action DisableEvent;
        void OnDisable(); // Called by Unity
    }
}