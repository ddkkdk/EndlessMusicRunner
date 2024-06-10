using DG.Tweening;
using Spine;
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
    public GameObject greatTxtEffect;
    private Collider2D selfCollider;
    public int score;
    public int comboScore;
    [Range(0.2f, 10f)]
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

        selfCollider = GetComponent<Collider2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Monster")
        {


            var monster = other.gameObject.GetComponent<Monster>();

            monster?.MonsterDamage(1);

            AudioManager.instance.PlaySound();

            //score++;
            //comboScore++;

            //UIManager.Instance.ScoreUpdater(score);
            //UIManager.Instance.ComboScoreUpdater(comboScore);
            UIManager.Instance.ComboScoreUpdater();
            UIManager.Instance.ScoreUpdater();
            Vector2 hitPoint = other.ClosestPoint(transform.position);
            var monsterPosition = other.gameObject.transform.position;
            if (hitEffect != null)
            {
                var distance = Vector3.Distance(transform.position, monsterPosition);
                if (distance <= 0.8f)
                {
                    GameObject hitObject = Instantiate(hitEffect, hitPoint, Quaternion.identity);
                    GameObject perfectTxtObject = Instantiate(perfectTxtEffect, hitPoint, Quaternion.identity);
                    StartCoroutine(OpacityChange(perfectTxtObject));

                    HIttingEffects(other.gameObject, hitPoint);

                    MoveUPword(perfectTxtObject, hitPoint);

                    //Destroy(hitObject, 0.2f);
                    //Destroy(perfectTxtObject, 0.8f);
                }
                else
                {
                    GameObject greatObject = Instantiate(hitEffect, hitPoint, Quaternion.identity);
                    GameObject greatTxtObject = Instantiate(greatTxtEffect, hitPoint, Quaternion.identity);
                    StartCoroutine(OpacityChange(greatTxtObject));

                    HIttingEffects(other.gameObject, hitPoint);

                    MoveUPword(greatTxtObject, hitPoint);

                    //Destroy(greatObject, 0.2f);
                    //Destroy(greatTxtObject, 0.8f);
                }


            }

            if (monster)
            {
                if (monster.currentHealth > 0)
                {
                    return;
                }
            }

            other.GetComponent<Collider2D>().enabled = false;
            other.GetComponent<Rigidbody2D>().isKinematic = false;
            other.GetComponent<MoveLeft>().speed = 0;

            float position = other.gameObject.transform.position.y;

            if (position > -8)
            {
                //Debug.Log("Hit Upper");
                other.GetComponent<Rigidbody2D>().AddForce(-transform.up * 50, ForceMode2D.Impulse);

            }

            GameManager.instance.PlayMonsterAnimation(other.GetComponent<SkeletonAnimation>());
            #region AnimationSettin ��
            //if (other.GetComponent<MoveLeft>().monsterNumber == 4)
            //{
            //    other.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "hit_fly_1", false);
            //}
            //if (other.GetComponent<MoveLeft>().monsterNumber == 6)
            //{
            //    //other.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "Hit", false);
            //    other.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "hit_fly_1", false);
            //}
            //else if (other.GetComponent<MoveLeft>().monsterNumber == 7)
            //{
            //    other.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "Hit", false);

            //}
            //else
            //{
            //    other.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "Hit", false);
            //    //other.GetComponent<SkeletonAnimation>().AnimationState.SetAnimation(0, "hit_fly_1", false); 
            //}

            #endregion 

        }

    }

    public void MoveUPword(GameObject perfectTxtEffect, Vector2 hitPoint)
    {
        perfectTxtEffect.transform.DOMoveY(hitPoint.y + 2, 0.1f);

    }
    public void HIttingEffects(GameObject other, Vector2 hitPoint)
    {
        //Debug.Log("fshfkshfkshfkshfsjkhfjwsk");
        int mNumber = other.GetComponent<MoveLeft>().monsterNumber;
        //Debug.Log("Monster number " + mNumber);

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

        var color = obj.GetComponent<SpriteRenderer>();
        Color currentColor = obj.GetComponent<SpriteRenderer>().color;


        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {

            if (obj == null || !obj.activeSelf)
                yield break;

            float normalizedTime = t / fadeDuration;


            currentColor.a = Mathf.Lerp(1, 0, normalizedTime);
            if (currentColor.a <= 0.1f)
            {
                currentColor.a = 0;
                color.color = currentColor;
                Destroy(obj);

                yield break;
            }

            if (obj != null)
                obj.GetComponent<SpriteRenderer>().color = currentColor;


            yield return null;
        }

    }
}
