using UnityEngine;
using Utils;
using Utils.Event;

namespace ItemsSystem.Strategy
{
    public class DoorLock : IStrategy
    {
        public void Use(GameObject objectInHand, GameObject doorLock)
        {
            objectInHand.gameObject.SetActive(false);
            doorLock.SetActive(false);
            Signals.Get<LockOpeningSignal>().Dispatch();
        }
    }
}