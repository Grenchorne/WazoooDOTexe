using System;
using UnityEngine;

namespace Adhaesii.WazoooDOTexe
{
    public class PlayerVerticalPeek
    {
        private readonly Settings settings;

        public PlayerVerticalPeek(Settings settings)
        {
            this.settings = settings;
        }

        private float t_peek;
        private float pos;
        private bool inputLastFrame;
        
        public float ProcessPeek(float input, float deltaTime)
        {
            float target = 0f;
            // Escape if no input
            if (Mathf.Approximately(input, 0))
            {
                inputLastFrame = false;
                return processPos_();
            }

            // Last state was unheld, so let's capture that state change
            if (!inputLastFrame)
            {
                inputLastFrame = true;
                t_peek = settings.Delay;
                return processPos_();
            }

            // tick down the peek delay
            t_peek -= deltaTime;
            
            // ensure we don't process the peek until after the delay is met
            if (t_peek > 0)
                return processPos_();;

            // return positive offset if positive input
            if (input > 0)
                target = settings.Distance;

            // return positive offset if negative input
            if (input < 0)
                target = -settings.Distance;

            return processPos_();

            float processPos_()
            {
                // ensure a position outside of the distance is not set
                if (pos < target)
                    pos = Mathf.Clamp(pos + deltaTime * settings.Acceleration, pos, target);

                else if (pos > target)
                    pos = Mathf.Clamp(pos - deltaTime * settings.Acceleration, target, pos);

                return pos;
            }
        }
        
        [Serializable]
        public class Settings
        {
            [SerializeField]
            private float _distance = 10f;

            public float Distance => _distance;

            [SerializeField]
            private float _delay = 0.4f;
            public float Delay => _delay;

            [SerializeField]
            private float _acceleration = 50f;
            public float Acceleration => _acceleration;
        }
    }
}
