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
        [SerializeField] private Transform creatureTransformAttack;
        [SerializeField] private Transform creatureTransform;
        [SerializeField] private float searchDistance;
        [SerializeField] private float rangeAttackCreature;
        [SerializeField] private float fovAngel;
        [SerializeField] private NavMeshAgent navMeshAgentCreature; 
        [SerializeField] private LayerMask playerMask;
        
        public override void InstallBindings()
        {
            Container.Bind<Creature>()
                .FromInstance(creature)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<Attacker>()
                .AsSingle()
                .WithArguments(navMeshAgentCreature, creatureTransformAttack, rangeAttackCreature, playerMask)
                .NonLazy();
            
            Container.Bind<Search>()
                .AsSingle()
                .WithArguments(navMeshAgentCreature, playerMask, creatureTransform, searchDistance, fovAngel)
                .NonLazy();
                
            Container.Bind<Random>()
                .AsSingle()
                .NonLazy();
        }
    }
}