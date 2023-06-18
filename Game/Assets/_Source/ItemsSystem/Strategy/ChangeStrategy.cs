using LevelSystem;

namespace ItemsSystem.Strategy
{
    public class ChangeStrategy
    {
        // private DoorLock _doorLock;
        // private DoorBoard _doorBoard;
        
        public IStrategy SwitchStrategy(InteractionObjectEnum itemEnum)
        {
            switch (itemEnum)
            {
                case InteractionObjectEnum.Lock:
                {
                    // return _doorLock;
                    return new DoorLock();
                }
                case InteractionObjectEnum.Board:
                {
                    // return _doorBoard;
                    return new DoorBoard();
                }
            }

            return null;
        }
    }
}