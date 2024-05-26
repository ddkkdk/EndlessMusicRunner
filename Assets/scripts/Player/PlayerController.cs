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
        MoveAnimation();
        playerRb = GetComponent<Rigidbody2D>();
       
        playerSkeletonAnimation = GetComponentInChildren<SkeletonAnimation>();

        StartCoroutine(RunAnimation());
        Physics.gravity*=gravityModifier;

        currentHealth = maxHealth;



    }   

    protected override void Update()
    {
        Debug.Log("Time :" + Time.time);
        if (Input.GetKeyDown(KeyCode.F) && isOnGround) 
        {
          
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            GameManager.instance.AnimationController(flyAnimation);
            isOnGround= false;

        

        }

        if (Input.GetKeyDown(KeyCode.J)) 
        {
                
            GameManager.instance.AnimationController(kickAnimation);
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
            yield return new WaitForSeconds(0.5f);

        }
    
    }

    public void MoveAnimation() 
    {
        transform.DOMoveX(-54, 2).SetEase(Ease.Flash).OnComplete(() =>
        {
            StartCoroutine(WatingTime());
           
        });
    }

    IEnumerator WatingTime() 
    {
        yield return new WaitForSeconds(1.5f);
        UIManager.Instance.ActivatPanel(true);

    }

    public void SetHealthBar()
    {
        
        fillAmount.fillAmount = currentHealth / maxHealth;


    }





}
