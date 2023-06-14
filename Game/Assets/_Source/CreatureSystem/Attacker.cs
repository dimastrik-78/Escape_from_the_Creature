using UnityEngine;
using PlayerSystem;
using UnityEngine.AI;

namespace CreatureSystem
{
    public class Attacker
    {
        private readonly Creature _creature;
        private readonly NavMeshAgent _agent;
        private readonly Transform _transform;
        private readonly float _rangeAttack;
        private readonly LayerMask _player;

        public Attacker(Creature creature, NavMeshAgent agent, Transform transform, float rangeAttack, LayerMask player)
        {
            _creature = creature;
            _agent = agent;
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

            _creature.enabled = false;
            _agent.enabled = false;
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