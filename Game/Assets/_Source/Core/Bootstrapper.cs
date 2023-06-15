using Cinemachine;
using CreatureSystem;
using PlayerSystem;
using UISystem.GameUI;
using UnityEngine;
using UnityEngine.UI;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Creature creature;
        [SerializeField] private GameUIView gameUIView;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button settingsButton;
        [SerializeField] private Button backButton;
        [SerializeField] private Button exitButton;
        
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Rigidbody playerRb;
        [SerializeField] private Transform startPosition;
        [SerializeField] private CanvasGroup canvasGroup;
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            GameUIController gameUIController = new GameUIController(gameUIView, continueButton, settingsButton, backButton, exitButton);
            player.LookOnItem += gameUIController.LookOnItem;
            player.NotLookOnItem += gameUIController.NotLookOnItem;
            player.OnPause += gameUIController.Pause;

            new Game(player, creature, virtualCamera, playerRb, startPosition, canvasGroup).LevelReset();
        }
    }
}
