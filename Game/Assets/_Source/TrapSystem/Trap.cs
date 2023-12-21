using System;
using TrapSystem.Data;
using UnityEngine;
using Utils;

namespace TrapSystem
{
    public class Trap : MonoBehaviour
    {
        [SerializeField] private TrapType _type;
        [SerializeField] private LayerMask _player;

        private TrapController _trapController;
        
        public void Construct(TrapController trapController)
        {
            _trapController = trapController;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_player.Contains(other.gameObject.layer))
            {
                _trapController.TrapActivation(_type, gameObject, transform);
            }
        }

        public TrapType GetTrapType() => _type;
    }
}