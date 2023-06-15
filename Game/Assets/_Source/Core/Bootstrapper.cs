using Cinemachine;
using CreatureSystem;
using PlayerSystem;
using UISystem.GameUI;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private Creature creature;

        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Rigidbody playerRb;
        [SerializeField] private Transform startPosition;
        [SerializeField] private CanvasGroup canvasGroup;

        [Inject] private GameUIController _gameUIController;
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            player.LookOnItem += _gameUIController.LookOnItem;
            player.NotLookOnItem += _gameUIController.NotLookOnItem;
            player.OnPause += _gameUIController.Pause;
            
            _gameUIController.AddEvent();

            new Game(player, creature, virtualCamera, playerRb, startPosition, canvasGroup).LevelReset();
        }
    }
}
