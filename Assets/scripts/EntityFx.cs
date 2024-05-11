using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFx : MonoBehaviour
{
    [Header("Materials info")]
    [SerializeField] private Material hitMat;
    private Material orginalMat;
    public float flashDuration;

    private SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        orginalMat = sr.material;
    }

    private IEnumerator FlashFx() 
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(flashDuration);
        sr.material = orginalMat;
    
    }
}
