using UnityEngine;
using Utils;
using Utils.Event;

namespace ItemsSystem.InteractionObjectStrategy
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
            doorLock.transform.parent = null;
            Signals.Get<LockOpeningSignal>().Dispatch();
        }
    }
}