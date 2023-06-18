using UnityEngine;

namespace ItemsSystem.Strategy
{
    public interface IStrategy
    {
        void Use(GameObject objectInHand, GameObject gameObject);
    }
}