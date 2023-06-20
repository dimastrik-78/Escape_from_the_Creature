using UnityEngine;
using Utils;

namespace LevelSystem
{
    public class TouchObject : MonoBehaviour
    {
        [SerializeField] private Rigidbody rb;
        [SerializeField] private LayerMask player;
        
        private void OnCollisionEnter(Collision other)
        {
            if (player.Contains(other.gameObject.layer))
            {
                rb.constraints = RigidbodyConstraints.None;
            }
        }
    }
}