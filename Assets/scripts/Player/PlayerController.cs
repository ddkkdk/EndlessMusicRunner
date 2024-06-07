using Spine;
using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine.UI;
using TMPro;

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

    //아래는 테스트 진행을 위한 변수들임 -> 추후 변경 예정
    [Header("TestInputKeyLog")]
    public TextMeshProUGUI testInputKey;
    public GameObject playerLimitJumpPosition;
    private bool isJumping = false;
    private float moveTime = 0f;
    public float test = 10f;

    protected override void Start()
    {
        testInputKey.gameObject.SetActive(false);
        MoveAnimation();
        playerRb = GetComponent<Rigidbody2D>();
        StartCoroutine(RunAnimation());
        Physics.gravity*=gravityModifier;

        currentHealth = maxHealth;



    }
    protected override void Update()
    {
        #region 플레이어 조작 테스트중
        //디테일 작업진행 필요함.
        //if(isJumping)
        //{
        //    moveTime += Time.deltaTime;
        //    float t = moveTime / test;
        //    transform.position = Vector2.Lerp(transform.position,playerLimitJumpPosition.transform.position,t);
        //    if(t>=0.5f)
        //    {
        //        Debug.Log("끝");
        //        isJumping = false;
        //        transform.position = playerLimitJumpPosition.transform.position;
        //    }
        //}
        //if (Input.GetKeyDown(KeyCode.Return) )
        //{
        //    isJumping = true;
        //}

        if (Input.GetKeyDown(KeyCode.UpArrow) && isGroundDetected())
        {
            //isJumping = true;
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse); //위치값 Lerp로 해서 그이상 못넘어가게 막기
            playerSkeletonAnimation.AnimationState.SetAnimation(0, flyAnimation, false).TimeScale = 7.5f;
            GameManager.instance.AnimationController(flyAnimation);
            GameObject.Find("AttackPoint_Up").GetComponent<Collider2D>().enabled = true;
            Invoke("UpperColliderDeactivate", 0.5f);
            movingEffect.SetActive(false);
            isOnGround = false;
            testInputKey.text = "Jump";
        }
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            testInputKey.gameObject.SetActive(true);
            testInputKey.text = "Sit Down";
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            testInputKey.gameObject.SetActive(true);
            testInputKey.text = "Sliding";
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            testInputKey.gameObject.SetActive(true);
            testInputKey.text = "Dash";
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            playerSkeletonAnimation.AnimationState.SetAnimation(0, kickAnimation, false).TimeScale = 2.5f;
            GameObject.Find("AttackPoint_Down").GetComponent<Collider2D>().enabled = true;
            Invoke("LowerColliderDeactivate", 0.5f);

            testInputKey.text ="Kick And Skill 1";
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            testInputKey.gameObject.SetActive(true);
            testInputKey.text = "Skill 2";
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            testInputKey.gameObject.SetActive(true);
            testInputKey.text = "Skill 3";
        }
        if (Input.GetKeyDown(KeyCode.V))
        {
            testInputKey.gameObject.SetActive(true);
            testInputKey.text = "Skill 4";
        }
        #endregion
    }

    protected override void FixedUpdate()
    {
       if(isOnGround)
            movingEffect.SetActive(true);
       
        if ((Input.GetKeyDown(KeyCode.F)) && isGroundDetected()) 
        {
           
            playerRb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
            playerSkeletonAnimation.AnimationState.SetAnimation(0, flyAnimation, false).TimeScale=7.5f;
            GameManager.instance.AnimationController(flyAnimation);
            GameObject.Find("AttackPoint_Up").GetComponent<Collider2D>().enabled = true;
            Invoke("UpperColliderDeactivate", 0.5f);
            movingEffect.SetActive(false);
            isOnGround = false;
        
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
