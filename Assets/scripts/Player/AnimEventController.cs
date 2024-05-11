using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEventController : MonoBehaviour
{
    
   private Player player =>GetComponentInParent<Player>();

    public void AnimEventTrigger() 
    {
        player.AnimationTrigger();
    }

    public void AttackTrigger() 
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackCheckRadius);

        foreach (var hit in colliders) 
        {
            if (hit.GetComponent<Enemy>() != null)
             hit.GetComponent<Enemy>().Damage();
        
        }
    
    
    
    }
}
