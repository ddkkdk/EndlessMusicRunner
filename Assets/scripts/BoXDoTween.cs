using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoXDoTween : MonoBehaviour
{
    public float scaleUpFactor = 1.5f; // The factor by which the object scales up
    public float duration = 0.5f; // Duration of the scaling up and down
    public float shakeDuration = 0.5f; // Duration of the shake effect
    public float shakeStrength = 1.0f; // Strength of the shake effect
    public int vibrato = 10; // Vibrato of the shake effect

    private Vector3 originalScale;

    void Start()
    {
        originalScale = transform.localScale;
        ScaleUpAndShake();
    }

    public void ScaleUpAndShake()
    {
        Sequence sequence = DOTween.Sequence();

        // Scale up
        sequence.Append(transform.DOScale(originalScale * scaleUpFactor, duration));

        // Scale back down
        sequence.Append(transform.DOScale(originalScale, duration));

        // Shake
        sequence.Append(transform.DOShakePosition(shakeDuration, shakeStrength, vibrato));

        sequence.Play();
    }
}
