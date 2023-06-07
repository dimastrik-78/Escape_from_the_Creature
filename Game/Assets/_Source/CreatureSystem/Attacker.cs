using UnityEngine;
using Utils.Event;
using Utils;
using PlayerSystem;

namespace CreatureSystem
{
    public class Attacker
    {
        private Transform _transform;
        private float _rangeAttack;
        private LayerMask _player;

        public Attacker(Transform transform, float rangeAttack, LayerMask player)
        {
            _transform = transform;
            _rangeAttack = rangeAttack;
            _player = player;
        }

        public void Attack(IGetDamage player)
        {
            if (!CheckRangeForAttack())
            {
                return;
            }

            player.GetDamage();
            //Signals.Get<LoseSignal>().Dispatch();
        }

        private bool CheckRangeForAttack()
        {
            if (Physics.Raycast(_transform.position, _transform.forward, _rangeAttack, _player))
            {
                return true;
            }

            return false;
        }
    }
}