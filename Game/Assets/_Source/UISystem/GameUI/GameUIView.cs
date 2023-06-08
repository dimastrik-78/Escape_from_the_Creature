using System;
using UnityEngine;
using Utils;
using Utils.Event;

namespace UISystem.GameUI
{
    public class GameUIView : MonoBehaviour
    {
        [SerializeField] private GameObject hint;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject settingsPanel;

        private void Awake()
        {
            Signals.Get<WinSignal>().AddListener(EndGame);
            Signals.Get<LoseSignal>().AddListener(EndGame);
        }

        public void HintEnable()
        {
            hint.SetActive(true);
        }

        public void HintDisable()
        {
            hint.SetActive(false);
        }

        public void PauseOn()
        {
            gamePanel.SetActive(false);
            pausePanel.SetActive(true);
            
            Cursor.lockState = CursorLockMode.None;
        }

        public void PauseOff()
        {
            gamePanel.SetActive(true);
            pausePanel.SetActive(false);
            
            Cursor.lockState = CursorLockMode.Locked;
        }

        public void OpenSettings()
        {
            settingsPanel.SetActive(true);
            pausePanel.SetActive(false);
        }

        public void CloseSettings()
        {
            settingsPanel.SetActive(false);
            pausePanel.SetActive(true);
        }

        private void EndGame()
        {
            gameObject.SetActive(false);
            
            Signals.Get<WinSignal>().RemoveListener(EndGame);
            Signals.Get<LoseSignal>().RemoveListener(EndGame);
        }
    }
}
