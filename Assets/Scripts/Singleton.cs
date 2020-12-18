using Sirenix.OdinInspector;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public abstract class Singleton<T> : SerializedMonoBehaviour where T : SerializedMonoBehaviour
    {
        private static T __instance;

        public static T Instance
        {
            get
            {
                if (__instance != null)
                    return __instance;
                
                __instance = FindObjectOfType<T>();
                if (__instance != null)
                    return __instance;
                
                __instance = new GameObject($"[Singleton] {typeof(T).Name}").AddComponent<T>();
                    return __instance;
            }
        }    
    }
}