using CreatureSystem;
using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Random = System.Random;

namespace Core.Installers
{
    public class CreatureInstaller : MonoInstaller
    {
        [SerializeField] private Creature creature;
        [SerializeField] private Transform creaturTransformAttack;
        [SerializeField] private Transform creaturTransform;
        [SerializeField] private float searchDistance;
        [SerializeField] private float rangeAttackCreature;
        [SerializeField] private float fovAngel;
        [SerializeField] private NavMeshAgent navMeshAgentCreature; 
        [SerializeField] private LayerMask playerMask;
        
        public override void InstallBindings()
        {
            Container.Bind<Attacker>()
                .AsSingle()
                .WithArguments(creature, navMeshAgentCreature, creaturTransformAttack, rangeAttackCreature, playerMask)
                .NonLazy();
            
            Container.Bind<Search>()
                .AsSingle()
                .WithArguments(navMeshAgentCreature, playerMask, creaturTransform, searchDistance, fovAngel)
                .NonLazy();
                
            Container.Bind<Random>()
                .AsSingle()
                .NonLazy();
        }
    }
}