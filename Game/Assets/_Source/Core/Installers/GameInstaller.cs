using UISystem.GameUI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private GameUIView view;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button exitButton;
        
        public override void InstallBindings()
        {
            Container.Bind<GameUIController>()
                .AsSingle()
                .WithArguments(view, continueButton, settingsButton, backButton, exitButton)
                .NonLazy();
        }
    }
}