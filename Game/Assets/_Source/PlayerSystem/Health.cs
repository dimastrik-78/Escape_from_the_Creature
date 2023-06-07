using System;
using Utils.Event;
using Utils;

namespace Assets._Source.PlayerSystem
{
    public class Health
    {
        private int _health;
        
        public Health(int health)
        {
            _health = health;
        }

        public void LostOneHP()
        {
            _health--;

            if (!CheckLive())
            {
                Signals.Get<LoseSignal>().Dispatch();
            }
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
