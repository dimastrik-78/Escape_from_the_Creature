using System;
using Utils.Event;
using Utils;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace PlayerSystem
{
    public class Health
    {
        private Transform _startPosition;
        private Transform _player;
        private int _health;
        
        public Health(Transform player, Transform startPosition, int health)
        {
            _player = player;
            _startPosition = startPosition;
            _health = health;
        }

        public void LostOneHP()
        {
            _health--;

            if (!CheckLive())
            {
                Signals.Get<LoseSignal>().Dispatch();
            }

            Time.timeScale = 1;
        }

        private bool CheckLive()
        {
            if (_health <= 0)
            {
                return false;
            }

            _player.position = _startPosition.position;
            return true;
        }
    }
}
