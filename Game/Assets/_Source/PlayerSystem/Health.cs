using Utils.Event;
using Utils;
using UnityEngine;

namespace PlayerSystem
{
    public class Health
    {
        private int _health;
        
        public Health(int health)
        {
            _health = health;
        }

        public int CountHealth => _health;

        public void LostOneHP()
        {
            _health--;

            if (!CheckLive())
            {
                Time.timeScale = 0;
                Signals.Get<LoseSignal>().Dispatch();
            }
            
            Signals.Get<PlayerGetDamageSignal>().Dispatch();
            
            Time.timeScale = 1;
        }

        private bool CheckLive()
        {
            if (_health <= 0)
            {
                return false;
            }

            return true;
        }
    }
}
