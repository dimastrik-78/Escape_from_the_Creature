using System.Collections;
using UnityEngine;
using Utils;
using Utils.Event;
using Zenject;

namespace PlayerSystem
{
    public class DamageReaction
    {
        private readonly Rigidbody _rb;

        [Inject] private Health _playerHealth;
        
        public DamageReaction(Rigidbody rb)
        {
            _rb = rb;
        }
        
        public IEnumerator PlayerTurnOnAnObject(Transform playerTransform, Transform enemy, LayerMask enemyMask)
        {
            while (!Physics.Raycast(playerTransform.position, playerTransform.forward, 4, enemyMask))
            {
                Vector3 direction = Vector3.RotateTowards(playerTransform.forward, enemy.position - playerTransform.position, 0.05f, 2);
                playerTransform.rotation = Quaternion.LookRotation(direction);
                
                yield return new WaitForSeconds(Time.deltaTime);
            }

            _rb.freezeRotation = false;
            Signals.Get<CloseEyeSignal>().Dispatch();
        }
    }
}