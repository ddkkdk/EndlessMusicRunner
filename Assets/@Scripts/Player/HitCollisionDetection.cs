using DG.Tweening;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class HitCollisionDetection : MonoBehaviour
{
    public static HitCollisionDetection Instance;
    public GameObject upHitEffect;
    public GameObject hitEffect;
    public GameObject[] destroyParticleEffects;
    public GameObject puffEffect;
    public GameObject[] ScroeStateList;

    [Range(0.2f, 10f)]
    public float fadeDuration = 2.0f;

    [SpineAnimation]
    public string HitAnimation;

    public Transform downHitPoint;
    public Transform upHitPoint;
    public float effectUpPositionY;
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


    void SetBoss(GameObject obj, ScoreManager.E_ScoreState perfect)
    {
        SetEffect(obj, perfect);
    }

    void SetEffect(GameObject obj, ScoreManager.E_ScoreState perfect)
    {
        AudioManager.instance.PlaySound();
        //Perfect / Great�϶��� ���� �ְ� ���� 
        var score = 0;
        if (perfect == ScoreManager.E_ScoreState.Perfect)
        {
            score = 3;
        }
        else if (perfect == ScoreManager.E_ScoreState.Great)
        {
            score = 1;
        }
        ScoreManager.instance.SetCombo_Add();
        ScoreManager.instance.SetCurrentScore(score);

        if (perfect == ScoreManager.E_ScoreState.Late || perfect == ScoreManager.E_ScoreState.Early)
        {
            return;
        }

        var hitPoint = obj.transform.position;
        // ��� ����Ʈ �ϴ�����Ʈ ����
        if (hitPoint.y > 0)
        {
            GameObject hitObject = Instantiate(upHitEffect, hitPoint, Quaternion.identity);
        }
        else
        {
            GameObject hitObject = Instantiate(hitEffect, hitPoint, Quaternion.identity);
        }
        
        
        var txteffects = ScroeStateList[(int)perfect];
        //����Ʈ ��ġ���� �ڵ�� ���� 
        CreatEffect(obj, hitPoint, txteffects, effectUpPositionY);

        if (!obj.name.Contains("Boss"))
            HIttingEffects(obj, hitPoint);

        //MoveUPword(txtobject, hitPoint);
    }

    public void SetHit(GameObject obj, ScoreManager.E_ScoreState state)
    {
        var tag = obj.tag;
        ScoreManager.instance.SetScoreState(state);
        if (obj.tag == "Monster")
        {
            SetEffect(obj, state);
        }
        else if (obj.tag == "Boss")
        {
            SetBoss(obj, state);
        }
    }


    public void MoveUPword(GameObject perfectTxtEffect, Vector2 hitPoint)
    {
        perfectTxtEffect.transform.DOMoveY(hitPoint.y + 2, 0.1f);

    }

    public void HIttingEffects(GameObject other, Vector2 hitPoint)
    {
        int mNumber = other.GetComponent<MoveLeft>().monsterNumber;

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

    private void CreatEffect(GameObject monster,Vector3 hitPoint,GameObject txteffects  ,float effectUpPositionY )
    {
        var monsterType = monster.GetComponent<MoveLeft>().uniqMonster;
        var effectPosition = Vector3.zero;
        if (hitPoint.y > 0 && monsterType == UniqMonster.Normal)
        {
            effectPosition = upHitPoint.position;
            effectPosition.y += effectUpPositionY;
        }
        else if (hitPoint.y > 0 && monsterType == UniqMonster.SendBack)
        {
            effectPosition = upHitPoint.position;
            effectPosition.y += effectUpPositionY;
        }
        else if (hitPoint.y < 0)
        {
            effectPosition = downHitPoint.position;
            effectPosition.y += effectUpPositionY;
        }
        GameObject txtobject = Instantiate(txteffects, effectPosition, Quaternion.identity);
        StartCoroutine(OpacityChange(txtobject));


        MoveUPword(txtobject, effectPosition);
    }
}
