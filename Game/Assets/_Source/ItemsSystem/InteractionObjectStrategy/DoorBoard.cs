using UnityEngine;

namespace ItemsSystem.InteractionObjectStrategy
{
    public class DoorBoard : IStrategy
    {
        private readonly AudioSource _source;
        
        public DoorBoard(AudioSource source)
        {
            _source = source;
        }
        
        public void Use(GameObject objectInHand, GameObject gameObject)
        {
            _source.Play();
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            gameObject.transform.parent = null;
        }
    }
}