using Cinemachine;
using UISystem.GameUI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameUIView view;
        [SerializeField] private CanvasGroup canvasGroup;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button exitButton;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Rigidbody playerRb;
        [SerializeField] private Transform startPosition;
        
        public override void InstallBindings()
        {
            Container.Bind<GameUIController>()
                .AsSingle()
                .WithArguments(view, continueButton, settingsButton, backButton, exitButton)
                .NonLazy();

            Container.Bind<Game>()
                .AsCached()
                .WithArguments(virtualCamera, playerRb, startPosition, canvasGroup)
                .NonLazy();
        }
    }
}