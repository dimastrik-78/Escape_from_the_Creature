using UnityEngine;

namespace Utils
{
    public static class MyExtensions
    {
        public static bool Contains(this LayerMask mask, int layer)
        {
            return mask == (mask | (1 << layer));
        }
    }
}