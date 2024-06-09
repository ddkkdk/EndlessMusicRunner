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
        GameManager.instance.AnimationController("retire");
        //GameManager.instance.PlayAnimation(playerSkeletonAnimation, "tail attack2", true);
       // StartCoroutine(FlashFxCor());
    
    }

    private IEnumerator FlashFxCor() 
    {
        GameManager.instance.AnimationController("retire");
        yield return new WaitForSeconds(0.1f);
        /*mesh.material = hitMat;
     
        mesh.material = orginalMat;*/


    }


   

}
 