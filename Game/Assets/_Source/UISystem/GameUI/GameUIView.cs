using System;
using DG.Tweening;
using PlayerSystem;
using TMPro;
using UnityEngine;
using Utils;
using Utils.Event;

namespace UISystem.GameUI
{
    public class GameUIView : MonoBehaviour
    {
        public event Action OnOpenEye;
        
        [SerializeField] private GameObject hint;
        [SerializeField] private TextMeshProUGUI hintText;
        [SerializeField] private GameObject gamePanel;
        [SerializeField] private GameObject pausePanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private CanvasGroup canvasGroupDarkening;
        [SerializeField] private CanvasGroup canvasGroupCountHealth;
        [SerializeField] private TMP_Text countPlayerHealth;

        private void OnEnable()
        {
            Signals.Get<WinSignal>().AddListener(EndGame);
            Signals.Get<LoseSignal>().AddListener(EndGame);
        }

        private void OnDisable()
        {
            Signals.Get<WinSignal>().RemoveListener(EndGame);
            Signals.Get<LoseSignal>().RemoveListener(EndGame);
        }

        public void HintEnable(string text)
        {
            hint.SetActive(true);
            hintText.text = $"{text} E";
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
        
        public void CloseSettings()
        {
            settingsPanel.SetActive(false);
            pausePanel.SetActive(true);
        }

        public void OpenSettings()
        {
            settingsPanel.SetActive(true);
            pausePanel.SetActive(false);
        }
        
        public void CloseEye(Health health)
        {
            canvasGroupDarkening.gameObject.SetActive(true);
            canvasGroupDarkening.DOFade(endValue: 1, 2f)
                .OnComplete( () => 
                {
                    health.LostOneHP();
                });
        }
        
        public void ShowPlayerHealth(int countHealth)
        {
            countPlayerHealth.text = $"Days left: {countHealth}";
            canvasGroupCountHealth.gameObject.SetActive(true);
            canvasGroupCountHealth.DOFade(1, 2).OnComplete(ClosePlayerHealth);
        }
        
        private void ClosePlayerHealth()
        {
            canvasGroupCountHealth.DOFade(0, 2).OnComplete(() =>
            {
                canvasGroupCountHealth.gameObject.SetActive(false);
                OpenEye();
            });
        }
        
        private void OpenEye()
        {
            canvasGroupDarkening.gameObject.SetActive(true);
            canvasGroupDarkening.DOFade(0, 2).OnComplete(() =>
            {
                canvasGroupDarkening.gameObject.SetActive(false);
                OnOpenEye?.Invoke();
            });
        }

        private void EndGame()
        {
            gameObject.SetActive(false);
        }
    }
}
