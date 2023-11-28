using System;
using System.Collections.Generic;
using CreatureSystem.State;
using CreatureSystem.StateMachine.State;

namespace CreatureSystem.StateMachine
{
    public class EnemyStateMachine : IEnemyStateMachine
    {
        private Dictionary<Type, IEnemyState> _states = new();

        public void CreateState()
        {
            _states = new Dictionary<Type, IEnemyState>
            {
                [typeof(Patrolling)] = new Patrolling(),
                [typeof(ResponseNoise)] = new ResponseNoise(),
                [typeof(Stalking)] = new Stalking()
            };
        }
    }
}