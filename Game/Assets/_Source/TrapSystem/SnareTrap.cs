using UnityEngine;
using Utils;
using Utils.Event;

namespace TrapSystem
{
    public class SnareTrap : ITrap
    {
        public SnareTrap()
        {
            
        }
        
        public void Use(GameObject gameObject, Transform transform = null)
        {
            Signals.Get<PlayerMadeSound>().Dispatch(transform);
            Signals.Get<StanPlayerEvent>().Dispatch();
        }
    }
}