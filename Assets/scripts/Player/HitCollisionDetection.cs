using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCollisionDetection : MonoBehaviour
{
    public static HitCollisionDetection Instance;
    public GameObject hitEffect;
    public GameObject[] destroyParticleEffects;
    public GameObject puffEffect;
    public GameObject perfectTxtEffect;
    public int score;
    public int comboScore;
    public float fadeDuration = 2.0f;

    [SpineAnimation]
    public string HitAnimation;


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
            comboScore++;

            UIManager.Instance.ScoreUpdater(score);
            UIManager.Instance.ComboScoreUpdater(comboScore);
            
            Vector2 hitPoint = other.ClosestPoint(transform.position);

            if (hitEffect != null) 
            {
              GameObject hitObject= Instantiate(hitEffect, hitPoint, Quaternion.identity);
              GameObject perfectTxtObject = Instantiate(perfectTxtEffect, hitPoint, Quaternion.identity);
              StartCoroutine(OpacityChange(perfectTxtObject));

              HIttingEffects(other.gameObject, hitPoint);

              MoveUPword(perfectTxtObject,hitPoint);

              Destroy(hitObject,0.2f);
              Destroy(perfectTxtObject, 0.8f);
             


            }

            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<Rigidbody2D>().isKinematic = false;
            other.GetComponent<MoveLeft>().speed = 0;

            float position = other.gameObject.transform.position.y;
            

            if (position > -8)
            {
                Debug.Log("Hit Upper");
                other.GetComponent<Rigidbody2D>().AddForce(-transform.up * 50, ForceMode2D.Impulse);

            }

           



            if (other.GetComponent<MoveLeft>().monsterNumber == 6)
            {
                other.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "hit_fly_1", false);

            }
            else if (other.GetComponent<MoveLeft>().monsterNumber == 7) 
            {
                other.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "Hit", false);

            }
            else if(other.GetComponent<MoveLeft>().monsterNumber==5)
            {
                other.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "Hit_Fly_1", false);

            }



        }
        
    }

    public void MoveUPword(GameObject perfectTxtEffect, Vector2 hitPoint) 
    {
        perfectTxtEffect.transform.DOMoveY(hitPoint.y+2, 0.1f);
    
    }

    public IEnumerator MonsterDestroy(GameObject other) 
    {
        yield return new WaitForSeconds(0.5f);
     

    }

    public void HIttingEffects(GameObject other, Vector2 hitPoint) 
    {
        Debug.Log("fshfkshfkshfkshfsjkhfjwsk");
        int mNumber = other.GetComponent<MoveLeft>().monsterNumber;
        Debug.Log("Monster number " + mNumber);


        if (mNumber == 0)
        {
            GameObject destroyEffects = Instantiate(destroyParticleEffects[0], hitPoint, Quaternion.identity);
            Destroy(destroyEffects, 0.5f);

        }
        else if (mNumber == 1) 
        {
            GameObject destroyEffects = Instantiate(destroyParticleEffects[1], hitPoint, Quaternion.identity);
            Destroy(destroyEffects, 0.5f);

        }
        else if (mNumber == 2)
        {
            GameObject destroyEffects = Instantiate(destroyParticleEffects[2], hitPoint, Quaternion.identity);
            Destroy(destroyEffects, 0.5f);
        }
        else if (mNumber == 3)
        {
            GameObject destroyEffects = Instantiate(destroyParticleEffects[3], hitPoint, Quaternion.identity);
            Destroy(destroyEffects, 0.5f);
        }
        else if (mNumber == 4)
        {
            GameObject destroyEffects = Instantiate(destroyParticleEffects[4], hitPoint, Quaternion.identity);
            Destroy(destroyEffects, 0.5f);

        }
        else if (mNumber == 5)
        {
            GameObject destroyEffects = Instantiate(destroyParticleEffects[5], hitPoint, Quaternion.identity);
            Destroy(destroyEffects, 0.5f);

        }
        else if (mNumber == 6)
        {
            GameObject destroyEffects = Instantiate(destroyParticleEffects[6], hitPoint, Quaternion.identity);
            Destroy(destroyEffects, 0.5f);
        }
      

    }

    public IEnumerator OpacityChange(GameObject obj)
    {

        Color currentColor = obj.GetComponent<SpriteRenderer>().color;


        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {

            float normalizedTime = t / fadeDuration;


            currentColor.a = Mathf.Lerp(1, 0, normalizedTime);


            obj.GetComponent<SpriteRenderer>().color = currentColor; 


            yield return null;
        }


        currentColor.a = 0;
        obj.GetComponent<SpriteRenderer>().color = currentColor;
        Destroy(obj);

    }
}
