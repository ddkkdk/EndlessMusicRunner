using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollisionDetection : MonoBehaviour
{
    public GameObject hitEffect;
    public GameObject perfectTxtEffect;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster") 
        {
            
            
            Vector2 hitPoint = other.ClosestPoint(transform.position);

            if (hitEffect != null) 
            {
              GameObject hitObject= Instantiate(hitEffect, hitPoint, Quaternion.identity);
              GameObject perfectTxtObject = Instantiate(perfectTxtEffect, hitPoint, Quaternion.identity);
              MoveUPword(perfectTxtObject,hitPoint);
              Destroy(hitObject,0.2f);
              Destroy(perfectTxtObject, 0.2f);

            }
            other.gameObject.SetActive(false);
                       
        
        }
        
    }

    public void MoveUPword(GameObject perfectTxtEffect, Vector2 hitPoint) 
    {
        perfectTxtEffect.transform.DOMoveY(hitPoint.y+5, 0.5f);
    
    }
}
