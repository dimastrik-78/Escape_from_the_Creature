using UnityEngine;

namespace ItemsSystem.InteractionObjectStrategy
{
    public interface IStrategy
    {
        void Use(GameObject objectInHand, GameObject gameObject);
    }
}