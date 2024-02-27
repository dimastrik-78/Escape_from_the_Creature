using CreatureSystem;
using TrapSystem;
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
        [SerializeField] private float rangeAttackCreature;
        [SerializeField] private NavMeshAgent navMeshAgentCreature; 
        [SerializeField] private LayerMask playerMask;
        [SerializeField] private GameObject _prefabBananaTrap;
        [SerializeField] private GameObject _prefabToyTrap;
        [SerializeField] private GameObject _prefabCronyTrap;
        
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
                .NonLazy();
                
            Container.Bind<Random>()
                .AsSingle()
                .NonLazy();

            Container.Bind<TrapController>()
                .AsSingle()
                .WithArguments(_prefabCronyTrap, _prefabToyTrap, _prefabBananaTrap)
                .NonLazy();
        }
    }
}