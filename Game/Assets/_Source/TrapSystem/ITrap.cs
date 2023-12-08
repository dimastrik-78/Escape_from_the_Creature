using UnityEngine;

namespace TrapSystem
{
    public interface ITrap
    {
        void Use(GameObject gameObject, Transform transform = null);
    }
}