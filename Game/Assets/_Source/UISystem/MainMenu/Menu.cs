using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UISystem.MainMenu
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject menuPanel;
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private Button play;
        [SerializeField] private Button settings;
        [SerializeField] private Button back;
        [SerializeField] private Button exit;
        [SerializeField] private CanvasGroup canvasGroup;

        private void Awake()
        {
            play.onClick.AddListener(Play);
            settings.onClick.AddListener(OpenSettings);
            back.onClick.AddListener(CloseSettings);
            exit.onClick.AddListener(Exit);
        }

        private void Play()
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(1, 2).OnComplete(() =>
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            });
        }

        private void OpenSettings()
        {
            menuPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }

        private void CloseSettings()
        {
            menuPanel.SetActive(true);
            settingsPanel.SetActive(false);
        }

        private void Exit()
        {
            Application.Quit();
        }
    }
}
