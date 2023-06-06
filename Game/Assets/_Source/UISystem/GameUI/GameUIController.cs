namespace UISystem.GameUI
{
    public class GameUIController
    {
        private readonly GameUIView _view;

        public GameUIController(GameUIView view)
        {
            _view = view;
        }

        public void LookOnItem()
        {
            _view.HintEnable();
        }

        public void NotLookOnItem()
        {
            _view.HintDisable();
        }
    }
}
