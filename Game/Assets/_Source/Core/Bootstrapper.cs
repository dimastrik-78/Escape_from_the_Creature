using PlayerSystem;
using UISystem.GameUI;
using UnityEngine;

namespace Core
{
    public class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private Player player;
        [SerializeField] private GameUIView gameUIView;

        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            GameUIController gameUIController = new GameUIController(gameUIView);
            player.LookOnItem += gameUIController.LookOnItem;
            player.NotLookOnItem += gameUIController.NotLookOnItem;
        }
    }
}
