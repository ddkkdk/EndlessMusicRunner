using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollisionDetection : MonoBehaviour
{
    public GameObject hitEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster") 
        {
            Debug.Log("Htt by player");
            Vector2 hitPoint = other.ClosestPoint(transform.position);

            if (hitEffect != null) 
            {
              GameObject hitObject= Instantiate(hitEffect, hitPoint, Quaternion.identity);
              Destroy(hitObject,0.2f);

            
            }
            other.gameObject.SetActive(false);
            
           
        
        }
        
    }
}
