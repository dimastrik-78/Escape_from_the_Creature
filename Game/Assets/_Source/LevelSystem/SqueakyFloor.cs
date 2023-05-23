using UnityEngine;
using Utils;
using Utils.Event;

namespace LevelSystem
{
    public class SqueakyFloor : MonoBehaviour
    {
        [SerializeField] private LayerMask player;

        private void OnTriggerEnter(Collider other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                Signals.Get<PlayerMadeSound>().Dispatch(transform);
            }
        }
    }
}