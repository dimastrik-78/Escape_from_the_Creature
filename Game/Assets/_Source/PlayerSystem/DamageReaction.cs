using System.Collections;
using UnityEngine;

namespace PlayerSystem
{
    public class DamageReaction
    {
        
        
        public IEnumerator PlayerTurnOnAnObject(Transform player, Transform enemy, LayerMask enemyMask)
        {
            while (!Physics.Raycast(player.position, player.forward, 4, enemyMask))
            {
                Vector3 direction = Vector3.RotateTowards(player.forward, enemy.position - player.position, 0.05f, 2);
                player.rotation = Quaternion.LookRotation(direction);
                
                yield return new WaitForSeconds(Time.deltaTime);
            }
            
            
        }
        
        // private int 
    }
}