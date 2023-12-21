using UnityEngine;
using Utils;
using Utils.Event;

namespace TrapSystem
{
    public class BananaTrap : ITrap
    {
        public void Use(GameObject gameObject, Transform transform = null)
        {
            Signals.Get<StanPlayerEvent>().Dispatch();
            gameObject.SetActive(false);
        }
    }
}