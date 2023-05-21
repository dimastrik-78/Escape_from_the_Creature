using UnityEngine;

namespace PlayerSystem
{
    public class Interaction
    {
        private Transform _hand;
        
        public Interaction(Transform hand)
        {
            _hand = hand;
        }

        public void Selection()
        {
            
        }

        public void Use()
        {
            Debug.Log("Try use item");
        }

        public void Drop()
        {
            
        }
    }
}