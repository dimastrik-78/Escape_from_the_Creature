using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UISystem.GameUI
{
    public class GameUIController
    {
        private readonly GameUIView _view;

        private Button _continueButton;
        private Button _settingsButton;
        private Button _backButton;
        private Button _exitButton;

        public GameUIController(GameUIView view, Button continueButton, Button settingsButton, Button backButton, Button exitButton)
        {
            _view = view;
            _continueButton = continueButton;
            _settingsButton = settingsButton;
            _backButton = backButton;
            _exitButton = exitButton;
            AddEvent();
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
            Time.timeScale = 0;
            _view.PauseOn();
        }

        private void AddEvent()
        {
            _continueButton.onClick.AddListener(Continue);
            _settingsButton.onClick.AddListener(OpenSettings);
            _backButton.onClick.AddListener(CloseSettings);
            _exitButton.onClick.AddListener(Exit);
        }

        private void Continue()
        {
            Time.timeScale = 1;
            _view.PauseOff();
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
