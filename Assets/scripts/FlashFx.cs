using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashFx : MonoBehaviour
{
    public static FlashFx Instance;
    private MeshRenderer mesh;

    [SerializeField] private Material hitMat;
    private Material orginalMat;
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
        mesh = GetComponentInChildren<MeshRenderer>();
        orginalMat = mesh.material;
    }

    public void callFlash() 
    {
        StartCoroutine(FlashFxCor());
    
    }

    private IEnumerator FlashFxCor() 
    {
        mesh.material = hitMat;
        yield return new WaitForSeconds(0.2f);
        mesh.material = orginalMat;
        GameManager.instance.AnimationController("retire");

    }


}
 