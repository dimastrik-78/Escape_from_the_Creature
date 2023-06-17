using System.Collections;
using DG.Tweening;
using UnityEngine;
using Zenject;

namespace PlayerSystem
{
    public class DamageReaction
    {
        private readonly Rigidbody _rb;
        private readonly CanvasGroup _canvasGroup;
        
        [Inject] private Health _playerHealth;
        
        public DamageReaction(Rigidbody rb, CanvasGroup canvasGroup)
        {
            _rb = rb;
            _canvasGroup = canvasGroup;
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
            Darkening();
        }

        private void Darkening()
        {
            _canvasGroup.gameObject.SetActive(true);
            _canvasGroup.DOFade(endValue: 1, 2f)
                .OnComplete( () => 
                {
                    _playerHealth.LostOneHP();
                });
        }
    }
}