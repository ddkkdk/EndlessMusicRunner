using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Entity
{
    public int damageAmount;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerController>().Damage(damageAmount);
           
        }

    }


}
