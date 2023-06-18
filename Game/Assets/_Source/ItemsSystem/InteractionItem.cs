using UnityEngine;

namespace ItemsSystem
{
    public class InteractionItem : MonoBehaviour
    {
        [SerializeField] private ItemEnum itemEnum;

        public ItemEnum GetItemEnum() 
            => itemEnum;
    }
}