using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utils;
using Utils.Event;

namespace UISystem.ResultUI
{
    public class ResultUIView : MonoBehaviour
    {
        [SerializeField] private GameObject panel;
        [SerializeField] private TMP_Text resultText;
        [SerializeField] private string winText;
        [SerializeField] private string loseText;
        [SerializeField] private Button resetButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            AddEvent();
        }

        private void Win()
        {
            Time.timeScale = 0;
            panel.SetActive(true);
            resultText.text = winText;
        }

        private void Lose()
        {
            Time.timeScale = 0;
            panel.SetActive(true);
            resultText.text = loseText;
        }

        private void AddEvent()
        {
            resetButton.onClick.AddListener(Reset);
            menuButton.onClick.AddListener(Menu);
            exitButton.onClick.AddListener(Exit);
            
            Signals.Get<WinSignal>().AddListener(Win);
            Signals.Get<LoseSignal>().AddListener(Lose);
        }

        private void RemoveEvent()
        {
            resetButton.onClick.RemoveListener(Reset);
            menuButton.onClick.RemoveListener(Menu);
            exitButton.onClick.RemoveListener(Exit);
            
            Signals.Get<WinSignal>().RemoveListener(Win);
            Signals.Get<LoseSignal>().RemoveListener(Lose);
        }
        
        private void Reset()
        {
            RemoveEvent();
            Time.timeScale = 1;
            SceneManager.LoadScene(1);
        }
        
        private void Menu()
        {
            RemoveEvent();
            Time.timeScale = 1;
            SceneManager.LoadScene(0);
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}
