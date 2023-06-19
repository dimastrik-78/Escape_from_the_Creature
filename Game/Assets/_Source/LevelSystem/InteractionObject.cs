using ItemsSystem;
using UnityEngine;

namespace LevelSystem
{
    public class InteractionObject : MonoBehaviour
    {
        [SerializeField] private InteractionObjectEnum objectEnum;
        [SerializeField] private ItemEnum canInteractionItem;

        public InteractionObjectEnum ObjectEnum => objectEnum;
        public ItemEnum CanInteractionItem => canInteractionItem;
    }
}