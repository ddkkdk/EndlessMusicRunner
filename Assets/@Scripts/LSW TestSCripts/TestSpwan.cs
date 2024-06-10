using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpwan : MonoBehaviour
{
    private float time = 0f;
    public float fadeDuration = 2f;
    Color prevColor;
    SpriteRenderer sprite;

    private void Awake()
    {
        prevColor = GetComponent<SpriteRenderer>().color;
        sprite = GetComponent<SpriteRenderer>();    
    }
    private void Update()
    {
        Color currentColor = GetComponent<SpriteRenderer>().color;

        float normalizedTime = time / fadeDuration;
        currentColor.a = Mathf.Lerp(1, 0, normalizedTime);
        if (currentColor.a <= 0.1f)
        {
            currentColor.a = 1f;
            prevColor = currentColor;
            //gameObject.SetActive(false);
            Destroy(this.gameObject);
        }
        sprite.color = currentColor;
        time += Time.time;
    }
}
