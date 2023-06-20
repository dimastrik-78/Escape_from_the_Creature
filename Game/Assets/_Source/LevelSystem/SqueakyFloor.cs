using UnityEngine;
using Utils;
using Utils.Event;

namespace LevelSystem
{
    public class SqueakyFloor : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private LayerMask player;

        private void OnTriggerEnter(Collider other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                source.Play();
                Signals.Get<PlayerMadeSound>().Dispatch(transform);
            }
        }
    }
}