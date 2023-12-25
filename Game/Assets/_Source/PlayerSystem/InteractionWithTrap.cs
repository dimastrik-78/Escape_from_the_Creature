using System.Collections;
using UnityEngine;

namespace PlayerSystem
{
    public class InteractionWithTrap
    {
        private Transform _transformCamera;
        private PlayerInput _playerInput;
        private float _timeForStan;
        private float _distance;
        private LayerMask _trap;
        
        public void SetParameters(Transform transformCamera, PlayerInput playerInput, float timeForStan, float distance,
            LayerMask trap)
        {
            _transformCamera = transformCamera;
            _playerInput = playerInput;
            _timeForStan = timeForStan;
            _distance = distance;
            _trap = trap;
        }

        public IEnumerator Stan()
        {
            _playerInput.Disable();

            yield return new WaitForSeconds(_timeForStan);
            
            _playerInput.Enable();
        }

        public void InfiniteStan()
        {
            _playerInput.Action.MoveX.Disable();
            _playerInput.Action.MoveZ.Disable();
        }

        public void Interaction()
        {
            if (Physics.Raycast(_transformCamera.position, _transformCamera.forward, out var hit, _distance, _trap))
            {
                hit.transform.gameObject.SetActive(false);
                _playerInput.Action.MoveX.Enable();
                _playerInput.Action.MoveZ.Enable();
            }
        }
    }
}