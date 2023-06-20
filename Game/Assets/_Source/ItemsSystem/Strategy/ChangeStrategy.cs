using LevelSystem;
using UnityEngine;

namespace ItemsSystem.Strategy
{
    public class ChangeStrategy
    {
        private readonly DoorLock _doorLock;
        private readonly DoorBoard _doorBoard;
        
        public ChangeStrategy(AudioSource openLock, AudioSource breakBoard)
        {
            _doorLock = new DoorLock(openLock);
            _doorBoard = new DoorBoard(breakBoard);
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