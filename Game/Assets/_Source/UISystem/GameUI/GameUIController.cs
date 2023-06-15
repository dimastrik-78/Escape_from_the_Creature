using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Zenject;

namespace UISystem.GameUI
{
    public class GameUIController
    {
        private readonly GameUIView _view;
        private readonly Button _continueButton;
        private readonly Button _settingsButton;
        private readonly Button _backButton;
        private readonly Button _exitButton;

        [Inject] private PlayerInput _input;

        private bool _pause;

        public GameUIController(GameUIView view, Button continueButton, Button settingsButton, Button backButton, Button exitButton)
        {
            _view = view;
            _continueButton = continueButton;
            _settingsButton = settingsButton;
            _backButton = backButton;
            _exitButton = exitButton;
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
            _input.Disable();
        }

        public void AddEvent()
        {
            _continueButton.onClick.AddListener(Continue);
            _settingsButton.onClick.AddListener(OpenSettings);
            _backButton.onClick.AddListener(CloseSettings);
            _exitButton.onClick.AddListener(Exit);
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
            _input.Enable();
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
            SceneManager.LoadScene(0);
        }
    }
}
