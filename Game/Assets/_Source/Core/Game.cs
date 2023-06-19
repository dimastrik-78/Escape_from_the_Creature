using System;
using Cinemachine;
using CreatureSystem;
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
        public event Action OnGameEnd;
        public event Action ShowHealth;
        
        private readonly CinemachineVirtualCamera _virtualCamera;
        private readonly Rigidbody _playerRb;
        private readonly Transform _startPosition;
        private readonly Transform[] _newPosition;
        private readonly NavMeshAgent _agent;
        
        [Inject] private readonly Player _player;
        [Inject] private PlayerInput _input;
        [Inject] private readonly Creature _creature;
        [Inject] private readonly Random _random;

        public Game(CinemachineVirtualCamera virtualCamera, Rigidbody playerRb, Transform startPosition, 
            Transform[] newPosition, NavMeshAgent agent)
        {
            _virtualCamera = virtualCamera;
            _playerRb = playerRb;
            _startPosition = startPosition;
            _newPosition = newPosition;
            _agent = agent;
        }

        private void LevelReset()
        {
            PlayerReset();
            CreatureReset();
            DisablePlayerInput();
            ShowHealth?.Invoke();
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

        public void StartGame()
        {
            Time.timeScale = 1;
            
            Cursor.lockState = CursorLockMode.Locked;
            
            Signals.Get<PlayerGetDamageSignal>().AddListener(LevelReset);
            Signals.Get<LoseSignal>().AddListener(EndGame);
            Signals.Get<WinSignal>().AddListener(EndGame);

            DisablePlayerInput();
            ShowHealth?.Invoke();
        }
        
        private void EndGame()
        {
            Time.timeScale = 0;
            
            Cursor.lockState = CursorLockMode.None;
            
            Signals.Get<PlayerGetDamageSignal>().RemoveListener(LevelReset);
            Signals.Get<LoseSignal>().RemoveListener(EndGame);
            Signals.Get<WinSignal>().RemoveListener(EndGame);
            
            OnGameEnd?.Invoke();
        }

        public void EnablePlayerInput()
        {
            _virtualCamera.enabled = true;
            _input.Enable();
        }

        private void DisablePlayerInput()
        {
            _virtualCamera.enabled = false;
            _input.Disable();
        }
    }
}