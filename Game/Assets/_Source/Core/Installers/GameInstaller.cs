using Cinemachine;
using ItemsSystem.InteractionObjectStrategy;
using UISystem.GameUI;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;

namespace Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [FormerlySerializedAs("view")]
        [Header("UI"), Space(5f)]
        [SerializeField] private GameUIView gameView;
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
        
        [Header("Audio"), Space(5f)] 
        [SerializeField] private AudioSource openLock;
        [SerializeField] private AudioSource breakBoard;
        
        public override void InstallBindings()
        {
            Container.Bind<GameUIController>()
                .AsSingle()
                .WithArguments(gameView, continueButton, settingsButton, backButton, exitButton)
                .NonLazy();
            
            Container.Bind<ChangeStrategy>()
                .AsSingle()
                .WithArguments(openLock, breakBoard);

            Container.Bind<Game>()
                .AsSingle()
                .WithArguments(virtualCamera, playerRb, startPosition, newPosition, agent)
                .NonLazy();
        }
    }
}