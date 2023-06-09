using UnityEngine;
using PlayerSystem;
using UnityEngine.AI;
using Zenject;

namespace CreatureSystem
{
    public class Attacker
    {
        private readonly NavMeshAgent _agent;
        private readonly Transform _transform;
        private readonly float _rangeAttack;
        private readonly LayerMask _player;
        
        [Inject] private readonly Creature _creature;
        
        public Attacker(NavMeshAgent agent, Transform transform, float rangeAttack, LayerMask player)
        {
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