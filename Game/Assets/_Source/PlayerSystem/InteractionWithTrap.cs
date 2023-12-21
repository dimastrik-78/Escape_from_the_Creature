using System.Collections;
using UnityEngine;

namespace PlayerSystem
{
    public class InteractionWithTrap
    {
        private PlayerInput _playerInput;
        private float _timeForStan;
        
        public void SetParameters(PlayerInput playerInput, float timeForStan)
        {
            _playerInput = playerInput;
            _timeForStan = timeForStan;
        }

        public IEnumerator Stan()
        {
            _playerInput.Disable();

            yield return new WaitForSeconds(_timeForStan);
            
            _playerInput.Enable();
        }
    }
}