using LevelSystem;

namespace ItemsSystem.Strategy
{
    public class ChangeStrategy
    {
        private readonly DoorLock _doorLock;
        private readonly DoorBoard _doorBoard;
            
        public ChangeStrategy()
        {
            _doorLock = new DoorLock();
            _doorBoard = new DoorBoard();
        }
        
        public IStrategy SwitchStrategy(InteractionObjectEnum itemEnum)
        {
            switch (itemEnum)
            {
                case InteractionObjectEnum.Lock:
                {
                    return _doorLock;
                }
                case InteractionObjectEnum.Board:
                {
                    return _doorBoard;
                }
            }

            return null;
        }
    }
}