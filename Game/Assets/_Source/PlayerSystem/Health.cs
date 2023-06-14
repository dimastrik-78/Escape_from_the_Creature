using Utils.Event;
using Utils;
using UnityEngine;

namespace PlayerSystem
{
    public class Health
    {
        private readonly Transform _player;
        
        private int _health;
        
        public Health(Transform player, int health)
        {
            _player = player;
            _health = health;
        }

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
