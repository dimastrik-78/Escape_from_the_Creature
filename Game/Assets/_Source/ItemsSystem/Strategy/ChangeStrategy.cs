using LevelSystem;

namespace ItemsSystem.Strategy
{
    public class ChangeStrategy
    {
        public IStrategy SwitchStrategy(InteractionObjectEnum itemEnum)
        {
            switch (itemEnum)
            {
                case InteractionObjectEnum.Lock:
                {
                    return new DoorLock();
                }
                case InteractionObjectEnum.Board:
                {
                    return new DoorBoard();
                }
            }

            return null;
        }
    }
}