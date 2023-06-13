using UnityEngine;
using PlayerSystem;

namespace CreatureSystem
{
    public class Attacker
    {
        private readonly Transform _transform;
        private readonly float _rangeAttack;
        private readonly LayerMask _player;

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
            Debug.Log("attack");
            player.GetDamage(_transform);
        }

        private bool CheckRangeForAttack()
        {
            if (Physics.Raycast(_transform.position - Vector3.up, _transform.forward, _rangeAttack, _player))
            {
                return true;
            }

            return false;
        }
    }
}