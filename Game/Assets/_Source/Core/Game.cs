using Cinemachine;
using CreatureSystem;
using PlayerSystem;
using UnityEngine;
using Utils;
using Utils.Event;

namespace Core
{
    public class Game
    {
        private readonly Player _player;
        private readonly Creature _creature;
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly Rigidbody _playerRb;
        private readonly Transform _startPosition;
        private readonly CanvasGroup _canvasGroup;

        public Game(Player player, Creature creature, CinemachineVirtualCamera virtualCamera, Rigidbody playerRb, Transform startPosition, CanvasGroup canvasGroup)
        {
            _player = player;
            _creature = creature;
            _virtualCamera = virtualCamera;
            _playerRb = playerRb;
            _startPosition = startPosition;
            _canvasGroup = canvasGroup;
            
            StartGame();
        }
        
        public void LevelReset()
        {
            _player.enabled = true;
            _creature.enabled = true;
            
            _virtualCamera.enabled = true;
            _playerRb.freezeRotation = true;

            Transform playerTransform = _playerRb.transform;
            playerTransform.rotation = Quaternion.Euler(0, 0, 0);
            playerTransform.position = _startPosition.position;
            
            _canvasGroup.alpha = 0;
        }

        private void StartGame()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            Signals.Get<PlayerGetDamageSignal>().AddListener(LevelReset);
            Signals.Get<LoseSignal>().AddListener(EndGame);
            Signals.Get<WinSignal>().AddListener(EndGame);
        }
        
        private void EndGame()
        {
            Cursor.lockState = CursorLockMode.None;
            
            Signals.Get<PlayerGetDamageSignal>().RemoveListener(LevelReset);
            Signals.Get<LoseSignal>().RemoveListener(EndGame);
            Signals.Get<WinSignal>().RemoveListener(EndGame);
        }
    }
}