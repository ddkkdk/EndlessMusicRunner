using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : Entity
{
    private Rigidbody2D playerRb;
    private SkeletonAnimation playerSkeletonAnimation;
    public float jumpForce;
 
    public bool isJump=false;
    public float gravityModifier;
     
    [SpineAnimation]
    public string runAnimation;

    [SpineAnimation]
    public string flyAnimation;

    [SpineAnimation]
    public string kickAnimation;


    protected override void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
       
        playerSkeletonAnimation = GetComponentInChildren<SkeletonAnimation>();

        StartCoroutine(RunAnimation());
        Physics.gravity*=gravityModifier;

    }   

    protected override void Update()
    {
        if (Input.GetKeyDown(KeyCode.F) && isOnGround) 
        {
          
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerSkeletonAnimation.AnimationState.SetAnimation(0, flyAnimation, false);
            isOnGround= false;
          
                      
        }

        if (Input.GetKeyDown(KeyCode.J)) 
        {
            playerSkeletonAnimation.AnimationState.SetAnimation(0, kickAnimation, false);
            GameObject.Find("AttackChecked").GetComponent<Collider2D>().enabled = true;
            Invoke("ColliderDeactivate", 0.5f);

        }

                   
    }

    public void ColliderDeactivate() 
    {
        GameObject.Find("AttackChecked").GetComponent<Collider2D>().enabled = false;

    }

  
    IEnumerator RunAnimation() 
    {
        while (true) 
        {
            if (isOnGround)
            {
                playerSkeletonAnimation.AnimationState.SetAnimation(0, runAnimation, true);
            }
            yield return new WaitForSeconds(0.2f);

        }
    
    }
     

   
}
