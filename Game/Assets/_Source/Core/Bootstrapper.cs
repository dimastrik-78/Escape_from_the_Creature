using CodeLockSystem;
using PlayerSystem;
using UISystem.GameUI;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private CodeLock codeLock;
        [SerializeField] private ButtonNumber[] buttonNumbers;
        
        [Inject] private Player _player;
        [Inject] private GameUIController _gameUIController;
        [Inject] private Game _game;
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _player.LookOnItem += _gameUIController.LookOnItem;
            _player.NotLookOnItem += _gameUIController.NotLookOnItem;
            _player.OnPause += _gameUIController.Pause;

            foreach (ButtonNumber button in buttonNumbers)
            {
                button.OnPress += codeLock.InputCode;
            }
            
            _gameUIController.AddEvent();
            _gameUIController.EnableInput += _game.EnablePlayerInput;

            _game.OnGameEnd += _gameUIController.RemoveEvent;
            _game.ShowHealth += _gameUIController.ShowCountPlayerHealth;
            _game.StartGame();
        }
    }
}
