using DG.Tweening;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpperHitCollisionDetection : MonoBehaviour
{
    public static UpperHitCollisionDetection Instance;
    public GameObject hitEffect;
    public GameObject[] destroyParticleEffects;
    public GameObject puffEffect;
    public GameObject perfectTxtEffect;
    private Collider2D selfCollider;
    public int score;
    public int comboScore;

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

        selfCollider = GetComponent<Collider2D>();
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
                               
               GameObject perfectTxtObject = Instantiate(perfectTxtEffect, hitPoint, Quaternion.identity);
               GameObject hitObject = Instantiate(hitEffect, hitPoint, Quaternion.identity);
               GameObject destroyEffects = Instantiate(destroyParticleEffects[Random.Range(0, 4)], hitPoint, Quaternion.identity);
               MoveUPword(perfectTxtObject, hitPoint);
               Destroy(perfectTxtObject, 0.8f);
               
               Destroy(hitObject, 0.2f);
               Destroy(destroyEffects, 0.5f);


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
            else
            {
                other.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "Hit_Fly_1", false);

            }


        }

    }

    public void MoveUPword(GameObject perfectTxtEffect, Vector2 hitPoint)
    {
        perfectTxtEffect.transform.DOMoveY(hitPoint.y + 2, 0.1f);

    }


}