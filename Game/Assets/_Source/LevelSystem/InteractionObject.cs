using UnityEngine;

namespace LevelSystem
{
    public class InteractionObject : MonoBehaviour
    {
        [SerializeField] private InteractionObjectEnum objectEnum;

        public InteractionObjectEnum GetItemEnum() 
            => objectEnum;
    }
}