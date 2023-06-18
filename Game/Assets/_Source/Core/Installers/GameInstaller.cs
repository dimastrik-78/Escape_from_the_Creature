using Cinemachine;
using ItemsSystem.Strategy;
using UISystem.GameUI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Zenject;

namespace Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Header("UI"), Space(5f)]
        [SerializeField] private GameUIView view;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button exitButton;
        
        [Header("Player"), Space(5f)]
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Rigidbody playerRb;
        [SerializeField] private Transform startPosition;

        [Header("Creature"), Space(5f)] 
        [SerializeField] private Transform[] newPosition;
        [SerializeField] private NavMeshAgent agent;
        
        public override void InstallBindings()
        {
            Container.Bind<GameUIController>()
                .AsSingle()
                .WithArguments(view, continueButton, settingsButton, backButton, exitButton)
                .NonLazy();
            
            Container.Bind<ChangeStrategy>()
                .AsSingle();

            Container.Bind<IStrategy>()
                .To<DoorLock>()
                .AsCached()
                .NonLazy();
            Container.Bind<IStrategy>()
                .To<DoorBoard>()
                .AsCached()
                .NonLazy();

            Container.Bind<Game>()
                .AsCached()
                .WithArguments(virtualCamera, playerRb, startPosition, canvasGroup, newPosition, agent)
                .NonLazy();
        }
    }
}