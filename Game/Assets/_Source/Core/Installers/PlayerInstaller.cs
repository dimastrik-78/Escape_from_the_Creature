using PlayerSystem;
using UnityEngine;
using Zenject;

namespace Core.Installers
{
    public class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private Player player;
        [SerializeField] private Rigidbody playerRb;
        [SerializeField] private CapsuleCollider playerCollider;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private int playerHp;
        [SerializeField] private int maxStamina;
        [SerializeField] private Transform hand;
        [SerializeField] private FixedJoint joint;
        [SerializeField] private AudioSource staminaFull;
        [SerializeField] private AudioSource cantRun;
        [SerializeField] private LayerMask wall;

        public override void InstallBindings()
        {
            Container.Bind<Player>()
                .FromInstance(player)
                .AsSingle()
                .NonLazy();
            
            Container.Bind<PlayerInput>()
                .AsSingle()
                .NonLazy();

            Container.Bind<Interaction>()
                .AsSingle()
                .WithArguments(hand, joint)
                .NonLazy();

            Container.Bind<Movement>()
                .AsSingle()
                .WithArguments(playerRb, playerTransform, playerCollider, wall)
                .NonLazy();
            
            Container.Bind<Health>()
                .AsSingle()
                .WithArguments(playerHp)
                .NonLazy();

            Container.Bind<Stamina>()
                .AsSingle()
                .WithArguments(maxStamina, staminaFull, cantRun)
                .NonLazy();
            
            Container.Bind<DamageReaction>()
                .AsSingle()
                .WithArguments(playerRb)
                .NonLazy();

            Container.Bind<InteractionWithTrap>()
                .AsSingle()
                .NonLazy();
        }
    }
}
