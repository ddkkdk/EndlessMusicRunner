using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollisionDetection : MonoBehaviour
{
    public static HitCollisionDetection Instance;
    public GameObject hitEffect;
    public GameObject perfectTxtEffect;
    public int score;

    private void Start()
    {
        if (Instance != null)
        {
            Destroy(this.gameObject);

        }
        else 
        {
            Instance = this;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster") 
        {
            AudioManager.instance.PlaySound();

            score++;
            UIManager.Instance.ScoreUpdater(score);
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
        perfectTxtEffect.transform.DOMoveY(hitPoint.y+10, 0.5f);
    
    }
}
