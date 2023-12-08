using UnityEngine;
using Utils;
using Utils.Event;

namespace TrapSystem
{
    public class PlushToyTrap : ITrap
    {
        public PlushToyTrap()
        {
            
        }

        public void Use(GameObject gameObject, Transform transform = null)
        {
            Debug.Log("test");
            Signals.Get<PlayerMadeSound>().Dispatch(transform);
            gameObject.SetActive(false);
        }
    }
}