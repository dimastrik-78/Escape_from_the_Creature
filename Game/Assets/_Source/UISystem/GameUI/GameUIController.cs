using System;
using PlayerSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using Utils.Event;
using Zenject;

namespace UISystem.GameUI
{
    public class GameUIController
    {
        public event Action EnableInput;

        private readonly GameUIView _view;
        private readonly Button _continueButton;
        private readonly Button _settingsButton;
        private readonly Button _backButton;
        private readonly Button _exitButton;

        [Inject] private readonly Health _health;

        private bool _pause;

        public GameUIController(GameUIView view, Button continueButton, Button settingsButton, Button backButton, Button exitButton)
        {
            _view = view;
            _continueButton = continueButton;
            _settingsButton = settingsButton;
            _backButton = backButton;
            _exitButton = exitButton;
        }

        public void ShowCountPlayerHealth()
        {
            _view.ShowPlayerHealth(_health.CountHealth);
        }

        public void LookOnItem()
        {
            _view.HintEnable();
        }

        public void NotLookOnItem()
        {
            _view.HintDisable();
        }

        public void Pause()
        {
            if (_pause)
            {
                return;
            }
            
            Time.timeScale = 0;
            _view.PauseOn();

            _pause = true;
        }

        public void AddEvent()
        {
            _continueButton.onClick.AddListener(Continue);
            _settingsButton.onClick.AddListener(OpenSettings);
            _backButton.onClick.AddListener(CloseSettings);
            _exitButton.onClick.AddListener(Exit);
            
            Signals.Get<CloseEyeSignal>().AddListener(CloseEye);
            
            _view.OnOpenEye += EnablePlayerInput;
        }

        public void RemoveEvent()
        {
            _continueButton.onClick.RemoveListener(Continue);
            _settingsButton.onClick.RemoveListener(OpenSettings);
            _backButton.onClick.RemoveListener(CloseSettings);
            _exitButton.onClick.RemoveListener(Exit);
            
            Signals.Get<CloseEyeSignal>().RemoveListener(CloseEye);
            
            _view.OnOpenEye -= EnablePlayerInput;
        }

        private void EnablePlayerInput() 
            => EnableInput?.Invoke();

        private void CloseEye()
        {
            _view.CloseEye(_health);
        }

        private void Continue()
        {
            if (!_pause)
            {
                return;
            }
            
            Time.timeScale = 1;
            _view.PauseOff();
            
            _pause = false;
            EnableInput?.Invoke();
        }

        private void OpenSettings()
        {
            _view.OpenSettings();
        }

        private void CloseSettings()
        {
            _view.CloseSettings();
        }

        private void Exit()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }
    }
}
