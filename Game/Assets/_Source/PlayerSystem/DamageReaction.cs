using System.Collections;
using UnityEngine;

namespace PlayerSystem
{
    public class DamageReaction
    {
        
        
        public IEnumerator PlayerTurnOnAnObject(Transform player, Transform enemy)
        {
            Vector3 direction = enemy.position - player.position;
            
            while (true)
            {
                player.rotation = Quaternion.Lerp(player.rotation, enemy.rotation, 5 * Time.deltaTime);
                yield return new WaitForSeconds(Time.deltaTime);
            }
        }
        
        // private int 
    }
}