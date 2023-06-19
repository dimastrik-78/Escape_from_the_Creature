using System;
using System.Collections;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    public class Stamina
    {
        public event Action OnStaminaOver;
        
        private readonly int _maxStamina;

        [Inject] private PlayerInput _input;

        private const float STAMINA_CHANGE_TIME = 0.1f;
        
        private float _residualStamina;

        public Stamina(int maxStamina)
        {
            _maxStamina = maxStamina;
            _residualStamina = _maxStamina;
        }

        public IEnumerator DecreasedStamina()
        {
            while (_residualStamina > 0)
            {
                if (_input.Action.MoveX.ReadValue<float>() != 0
                    || _input.Action.MoveZ.ReadValue<float>() != 0)
                {
                    _residualStamina -= STAMINA_CHANGE_TIME;
                }

                yield return new WaitForSeconds(STAMINA_CHANGE_TIME);
            }
            
            OnStaminaOver?.Invoke();
        }

        public IEnumerator RestoringStamina()
        {
            while (_residualStamina < _maxStamina)
            {
                _residualStamina += STAMINA_CHANGE_TIME;
                yield return new WaitForSeconds(STAMINA_CHANGE_TIME);
            }
        }
    }
}