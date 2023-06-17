using PlayerSystem;
using UISystem.GameUI;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [Inject] private Player _player;
        [Inject] private GameUIController _gameUIController;
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            _player.LookOnItem += _gameUIController.LookOnItem;
            _player.NotLookOnItem += _gameUIController.NotLookOnItem;
            _player.OnPause += _gameUIController.Pause;
            
            _gameUIController.AddEvent();
        }
    }
}
