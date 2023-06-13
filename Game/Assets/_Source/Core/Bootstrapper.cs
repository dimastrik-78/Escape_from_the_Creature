using PlayerSystem;
using UISystem.GameUI;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private GameUIView gameUIView;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button exitButton;

        private void Awake()
        {
            // Cursor.lockState = CursorLockMode.Locked;
            Init();
        }

        private void Init()
        {
            GameUIController gameUIController = new GameUIController(gameUIView, continueButton, settingsButton, backButton, exitButton);
            player.LookOnItem += gameUIController.LookOnItem;
            player.NotLookOnItem += gameUIController.NotLookOnItem;
            player.OnPause += gameUIController.Pause;
        }
    }
}
