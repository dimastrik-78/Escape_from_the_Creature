using Cinemachine;
using CreatureSystem;
using DG.Tweening;
using PlayerSystem;
using UnityEngine;
using UnityEngine.AI;
using Utils;
using Utils.Event;
using Zenject;
using Random = System.Random;

namespace Core
{
    public class Game
    {
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly Rigidbody _playerRb;
        private readonly Transform _startPosition;
        private readonly CanvasGroup _canvasGroup;
        private readonly Transform[] _newPosition;
        private readonly NavMeshAgent _agent;
        
        [Inject] private readonly Player _player;
        [Inject] private readonly Creature _creature;
        [Inject] private readonly Random _random;

        public Game(CinemachineVirtualCamera virtualCamera, Rigidbody playerRb, Transform startPosition, CanvasGroup canvasGroup, 
            Transform[] newPosition, NavMeshAgent agent)
        {
            _virtualCamera = virtualCamera;
            _playerRb = playerRb;
            _startPosition = startPosition;
            _canvasGroup = canvasGroup;
            _newPosition = newPosition;
            _agent = agent;
            
            StartGame();
        }

        private void LevelReset()
        {
            PlayerReset();
            CreatureReset();
            OpenEye();
        }

        private void PlayerReset()
        {
            _player.enabled = true;

            _virtualCamera.enabled = true;
            _playerRb.freezeRotation = true;

            Transform playerTransform = _playerRb.transform;
            playerTransform.rotation = Quaternion.Euler(0, 0, 0);
            playerTransform.position = _startPosition.position;
        }

        private void CreatureReset()
        {
            _creature.enabled = true;
            _creature.transform.position = _newPosition[_random.Next(0, _newPosition.Length)].position;
            
            _agent.enabled = true;
        }

        private void StartGame()
        {
            Cursor.lockState = CursorLockMode.Locked;
            
            Signals.Get<PlayerGetDamageSignal>().AddListener(LevelReset);
            Signals.Get<LoseSignal>().AddListener(EndGame);
            Signals.Get<WinSignal>().AddListener(EndGame);

            OpenEye();
        }
        
        private void EndGame()
        {
            Cursor.lockState = CursorLockMode.None;
            
            Signals.Get<PlayerGetDamageSignal>().RemoveListener(LevelReset);
            Signals.Get<LoseSignal>().RemoveListener(EndGame);
            Signals.Get<WinSignal>().RemoveListener(EndGame);
        }

        private void OpenEye()
        {
            _canvasGroup.DOFade(0, 2).OnComplete(() =>
            {
                _canvasGroup.gameObject.SetActive(false);
            });
        }
    }
}