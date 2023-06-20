using UnityEngine;
using Utils;
using Utils.Event;

namespace ItemsSystem
{
    public class FallItem : MonoBehaviour
    {
        [SerializeField] private AudioSource source;
        [SerializeField] private LayerMask floorMask;
        
        private void OnCollisionEnter(Collision other)
        {
            if (floorMask.Contains(other.gameObject.layer))
            {
                source.Play();
                Signals.Get<PlayerMadeSound>().Dispatch(transform);
            }
        }
    }
}
