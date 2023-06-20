using UnityEngine;
using Utils;
using Utils.Event;

namespace ItemsSystem.Strategy
{
    public class DoorLock : IStrategy
    {
        private readonly AudioSource _source;
        
        public DoorLock(AudioSource source)
        {
            _source = source;
        }
        
        public void Use(GameObject objectInHand, GameObject doorLock)
        {
            _source.Play();
            objectInHand.gameObject.SetActive(false);
            doorLock.SetActive(false);
            Signals.Get<LockOpeningSignal>().Dispatch();
        }
    }
}