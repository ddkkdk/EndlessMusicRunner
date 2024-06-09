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

    public bool isJump = false;
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

    public int comboCounter;
    public float lastTimeAttack;
    public float comboWindow = 3;
    public float combowDelay;


    //Ʒ ׽Ʈ ->
    [Header("TestInputKeyLog")]
    public TextMeshProUGUI testInputKey;
    public GameObject playerLimitJumpPosition;
    private bool isJumping = false;
    private float moveTime = 0f;
    public float test = 10f;
    private float isDashTime;
    public float isDashTimeLimit = 1f;
    public bool isDashing = false;
    public List<KeyCode> playerMoveKeyCode;
    public bool isAttack = false;
    float KickDealy;
    public static bool CheckHold;
    protected override void Start()
    {
        testInputKey.gameObject.SetActive(false);
        MoveAnimation();
        playerRb = GetComponent<Rigidbody2D>();
        Physics.gravity *= gravityModifier;

        currentHealth = maxHealth;



    }
    protected override void Update()
    {
        combowDelay -= Time.deltaTime;

        #region playerControllerAction Up/Sit down 
        PlayerMoveKey();
        #endregion
        if (comboCounter >= 3 || Time.time >= lastTimeAttack + comboWindow || combowDelay <= 0) 
        {
            comboCounter = 0;
            combowDelay = 1.95f;

        }
           
    }

    protected override void FixedUpdate()
    {
        if (isOnGround)
            movingEffect.SetActive(true);

        if (Input.GetKeyDown(KeyCode.F) && isGroundDetected() && isRunning)
        {

            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            playerSkeletonAnimation.AnimationState.SetAnimation(0, flyAnimation, false).TimeScale = 7.5f;
            GameObject.Find("AttackPoint_Up").GetComponent<Collider2D>().enabled = true;
            Invoke("UpperColliderDeactivate", 0.5f);
            movingEffect.SetActive(false);
            isOnGround = false;
            isRunning = false;

        }

        //    playerRb.AddForce(Vector2.up * jumpForce,ForceMode2D.Impulse);
        //    playerSkeletonAnimation.AnimationState.SetAnimation(0, flyAnimation, false).TimeScale=7.5f;
        //    GameManager.instance.AnimationController(flyAnimation);
        //    GameObject.Find("AttackPoint_Up").GetComponent<Collider2D>().enabled = true;
        //    Invoke("UpperColliderDeactivate", 0.5f);
        //    movingEffect.SetActive(false);
        //    isOnGround = false;

        //}

        //if (Input.GetKeyDown(KeyCode.J)) 
        //{
        //    playerSkeletonAnimation.AnimationState.SetAnimation(0, kickAnimation, false).TimeScale=2.5f;
        //    GameObject.Find("AttackPoint_Down").GetComponent<Collider2D>().enabled = true;
        //    Invoke("LowerColliderDeactivate", 0.5f);

        //}
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

    public void MoveAnimation()
    {
        transform.DOMoveX(-56, 2).SetEase(Ease.Flash).OnComplete(() =>
        {
            if (isStart)
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


    float HoldDelay = 0.5f;
    float Cur_HoldDelay = 0f;
    bool StartAniHold = false;

    private void PlayerMoveKey()
    {
        KickDealy -= Time.deltaTime;
        if (isDashing)
        {
            if (Time.time >= isDashTime + isDashTimeLimit)
            {
                isDashing = false;
                Time.timeScale = 1f;
            }
        }

        if (Input.GetKeyDown(playerMoveKeyCode[0]) && isGroundDetected())
        {
            playerRb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
           // playerSkeletonAnimation.AnimationState.SetAnimation(0, flyAnimation, false).TimeScale = 7.5f;
            GameObject.Find("AttackPoint_Up").GetComponent<Collider2D>().enabled = true;
            Invoke("UpperColliderDeactivate", 0.5f);
            movingEffect.SetActive(false);
            isOnGround = false;
            testInputKey.text = "Jump";
            GameManager.instance.PlayAnimation(playerSkeletonAnimation, "fly");
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            testInputKey.gameObject.SetActive(true);
            testInputKey.text = "Sit Down";
            GameManager.instance.PlayAnimation(playerSkeletonAnimation, "fly");
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            testInputKey.gameObject.SetActive(true);
            testInputKey.text = "Sliding";
            GameManager.instance.PlayAnimation(playerSkeletonAnimation, "fly");
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            testInputKey.gameObject.SetActive(true);
            testInputKey.text = "Dash";
            isDashing = true;
            isDashTime = Time.time;
            Time.timeScale = 1.5f;
            GameManager.instance.PlayAnimation(playerSkeletonAnimation, "Running");
        }

        //홀드키 땠을때
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (Cur_HoldDelay >= HoldDelay)
            {
                GameManager.instance.PlayAnimation(playerSkeletonAnimation, "Walking", true);
            }
            CheckHold = false;
            Cur_HoldDelay = 0;
            StartAniHold = false;


        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            comboCounter++;
            lastTimeAttack=Time.time;

         

            if (!isGroundDetected())
            {
              
                GameManager.instance.PlayAnimation(playerSkeletonAnimation, "fire attack");
                return;
            }


            if (KickDealy <= 0 && comboCounter == 1)
            {
                GameManager.instance.PlayAnimation(playerSkeletonAnimation, "Kick");
            }
            if (comboCounter == 2)
            {
                GameManager.instance.PlayAnimation(playerSkeletonAnimation, "tail attack");
            }
            if(comboCounter==3)
            {
                GameManager.instance.PlayAnimation(playerSkeletonAnimation, "tail attack2");
            }
            GameObject.Find("AttackPoint_Down").GetComponent<Collider2D>().enabled = true;
            Invoke("LowerColliderDeactivate", 0.5f);
            KickDealy = 1;
            testInputKey.text = "Kick And Skill 1";
        }
        //홀드중 
        if (Input.GetKey(KeyCode.J))
        {
            CheckHold = true;
            GameObject.Find("AttackPoint_Down").GetComponent<Collider2D>().enabled = true;
          

            Cur_HoldDelay += Time.deltaTime;
           

            if (Cur_HoldDelay >= HoldDelay && !StartAniHold)
            {
                LowerColliderDeactivate();

                GameManager.instance.PlayAnimation(playerSkeletonAnimation, "tail attack2", false);
                StartAniHold = true;
            }
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

    }


}