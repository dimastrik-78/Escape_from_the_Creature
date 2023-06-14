using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace PlayerSystem
{
    public class DamageReaction
    {
        private readonly Health _playerHealth;
        
        public DamageReaction(Health playerHealth)
        {
            _playerHealth = playerHealth;
        }
        
        public IEnumerator PlayerTurnOnAnObject(CanvasGroup canvasGroup, Rigidbody rb, Transform playerTransform, Transform enemy, LayerMask enemyMask)
        {
            while (!Physics.Raycast(playerTransform.position, playerTransform.forward, 4, enemyMask))
            {
                Vector3 direction = Vector3.RotateTowards(playerTransform.forward, enemy.position - playerTransform.position, 0.05f, 2);
                playerTransform.rotation = Quaternion.LookRotation(direction);
                
                yield return new WaitForSeconds(Time.deltaTime);
            }

            rb.freezeRotation = false;
            Darkening(canvasGroup);
        }

        private void Darkening(CanvasGroup canvasGroup)
        {
            canvasGroup.gameObject.SetActive(true);
            canvasGroup.DOFade(endValue: 1, 2f)
                .OnComplete( () => 
                {
                    _playerHealth.LostOneHP();
                });
        }
    }
}