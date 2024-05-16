using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D playerRb;
    private SkeletonAnimation playerSkeletonAnimation;
    public float jumpForce;
    private bool isOnGround=true;
    public bool isJump=false;
    public float gravityModifier;
    

    [Header("Collision Info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround;

    [SpineAnimation]
    public string runAnimation;

    [SpineAnimation]
    public string flyAnimation;

    [SpineAnimation]
    public string kickAnimation;


    private void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
       
        playerSkeletonAnimation = GetComponentInChildren<SkeletonAnimation>();

        StartCoroutine(RunAnimation());
        Physics.gravity*=gravityModifier;

    }   

    private void Update()
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

        }

                   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
        Debug.Log(isOnGround);
    }

    IEnumerator RunAnimation() 
    {
        while (true) 
        {
            if (isOnGround)
            {
                playerSkeletonAnimation.AnimationState.SetAnimation(0, runAnimation, true);
            }
            yield return new WaitForSeconds(0.5f);

        }
    
    }
     

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x, transform.position.y - groundCheckDistance));
    }
}
