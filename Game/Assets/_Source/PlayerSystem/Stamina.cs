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
        private readonly AudioSource _staminaFull;
        private readonly AudioSource _cantRun;

        public Stamina(int maxStamina, AudioSource staminaFull, AudioSource cantRun)
        {
            _maxStamina = maxStamina;
            _residualStamina = _maxStamina;

            _staminaFull = staminaFull;
            _cantRun = cantRun;
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
            
            _cantRun.Play();
            OnStaminaOver?.Invoke();
        }

        public IEnumerator RestoringStamina()
        {
            while (_residualStamina < _maxStamina)
            {
                _residualStamina += STAMINA_CHANGE_TIME;
                yield return new WaitForSeconds(STAMINA_CHANGE_TIME);
            }
            
            _cantRun.Stop();
            _staminaFull.Play();
        }
    }
}