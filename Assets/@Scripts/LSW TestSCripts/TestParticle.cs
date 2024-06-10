using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestParticle : MonoBehaviour
{
    public ParticleSystem particle;
    public GameObject effectObject;
    public float fadeDuration = 3f;
    private float time = 0f;

    private void Update()
    {
    }
    private void OnEnable()
    {
        Instantiate(effectObject,Vector3.zero,Quaternion.identity);
        time = 0f;
    }
    private void OnDisable()
    {
        gameObject.SetActive(false);
    }
}
