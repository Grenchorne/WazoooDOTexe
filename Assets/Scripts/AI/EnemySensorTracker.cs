using System;
using Sirenix.OdinInspector;

namespace Adhaesii.WazoooDOTexe.AI
{
    public class EnemySensorTracker : SerializedMonoBehaviour
    {
        [NonSerialized]
        public bool seen;

        public event Action OnSee;

        public void See()
        {
            if(seen)
                return;
            seen = true;
            OnSee?.Invoke();
        }
    }
}