using UnityEngine;

namespace ItemsSystem.Strategy
{
    public class DoorBoard : IStrategy
    {
        public void Use(GameObject objectInHand, GameObject gameObject)
        {
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();
            rb.constraints = RigidbodyConstraints.None;
            gameObject.transform.parent = null;
        }
    }
}