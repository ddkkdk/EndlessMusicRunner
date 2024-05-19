using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollisionDetection : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster") 
        {
            Debug.Log("Htt by player");
            other.gameObject.SetActive(false);
           
        
        }
        
    }
}
