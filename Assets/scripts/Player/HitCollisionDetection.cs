using DG.Tweening;
using Spine.Unity;
using System.Collections;
using UnityEngine;

public class HitCollisionDetection : MonoBehaviour
{
    public static HitCollisionDetection Instance;
    public GameObject hitEffect;
    public GameObject[] destroyParticleEffects;
    public GameObject puffEffect;
    public GameObject perfectTxtEffect;
    public GameObject greatTxtEffect;
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


    }


    void SetBoss(GameObject obj, bool perfect)
    {
        SetEffect(obj, perfect);
    }

    void SetEffect(GameObject obj, bool perfect)
    {
        AudioManager.instance.PlaySound();
        UIManager.Instance.ComboScoreUpdater();
        UIManager.Instance.ScoreUpdater();

        var txteffects = greatTxtEffect;

        if (perfect)
        {
            txteffects = perfectTxtEffect;
        }

        var hitPoint = obj.transform.position;

        GameObject hitObject = Instantiate(hitEffect, hitPoint, Quaternion.identity);
        GameObject txtobject = Instantiate(txteffects, hitPoint, Quaternion.identity);

        StartCoroutine(OpacityChange(txtobject));

        HIttingEffects(obj, hitPoint);

        MoveUPword(txtobject, hitPoint);
    }

    public void SetHit(GameObject obj, bool perfact)
    {
        var tag = obj.tag;

        if (obj.tag == "Monster")
        {
            SetEffect(obj, perfact);
        }
        else if (obj.tag == "Boss")
        {
            SetBoss(obj, perfact);
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
