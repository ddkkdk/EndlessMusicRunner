using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.UI;

public class PlayerController : Entity
{
    [Header("Collision Info")]

    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected LayerMask whatIsGround;
    private Rigidbody2D playerRb;
    public SkeletonAnimation playerSkeletonAnimation;
    public float jumpForce;
 
    public bool isJump=false;
    public float gravityModifier;
     
    [SpineAnimation]
    public string runAnimation;

    [SpineAnimation]
    public string flyAnimation;

    [SpineAnimation]
    public string kickAnimation;

    public float runningTimeScale;
    public GameObject movingEffect;

    public bool isStart;
    public bool isRunning;

  
    protected override void Start()
    {
        MoveAnimation();
        playerRb = GetComponent<Rigidbody2D>();
        StartCoroutine(RunAnimation());
        Physics.gravity*=gravityModifier;

        currentHealth = maxHealth;

        

    }   

    protected override void FixedUpdate()
    {
       if(isOnGround)
            movingEffect.SetActive(true);

        if (Input.GetKeyDown(KeyCode.F) && isGroundDetected() && isRunning) 
        {
           
            playerRb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            playerSkeletonAnimation.AnimationState.SetAnimation(0, flyAnimation, false).TimeScale=7.5f;
            GameManager.instance.AnimationController(flyAnimation);
            GameObject.Find("AttackPoint_Up").GetComponent<Collider2D>().enabled = true;
            Invoke("UpperColliderDeactivate", 0.5f);
            movingEffect.SetActive(false);
            isOnGround = false;
            isRunning=false;
        
        }

        if (Input.GetKeyDown(KeyCode.J)) 
        {
            playerSkeletonAnimation.AnimationState.SetAnimation(0, kickAnimation, false).TimeScale=2.5f;
            GameObject.Find("AttackPoint_Down").GetComponent<Collider2D>().enabled = true;
            Invoke("LowerColliderDeactivate", 0.5f);

        }
                  
    }

    public void ColliderDeactivate() 
    {
        GameObject.Find("AttackChecked").GetComponent<Collider2D>().enabled = false;

    }
    public void UpperColliderDeactivate()
    {
        GameObject.Find("AttackPoint_Up").GetComponent<Collider2D>().enabled = false;

    }
    public void LowerColliderDeactivate()
    {
        GameObject.Find("AttackPoint_Down").GetComponent<Collider2D>().enabled = false;

    }


    IEnumerator RunAnimation() 
    {
        while (true) 
        {
            if (isGroundDetected())
            {
                playerSkeletonAnimation.AnimationState.SetAnimation(0, runAnimation, true).TimeScale= runningTimeScale;
            }

            yield return new WaitForSeconds(0.5f);

        }
    
    }

    public void MoveAnimation() 
    {
        transform.DOMoveX(-56, 2).SetEase(Ease.Flash).OnComplete(() =>
        {
            if(isStart)
              StartCoroutine(WatingTime());
           
        });
    }

    IEnumerator WatingTime() 
    {
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.ActivatPanel(true);
        UIManager.Instance.attackPoints.SetActive(true);

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
    }

    public bool isGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);







}
