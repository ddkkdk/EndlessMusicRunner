 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision Info")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;

    public Transform attackCheck;
    public float attackCheckRadius;

    [Header("Knockback Info")]
    [SerializeField] protected Vector2 knockbackDirection;
    [SerializeField] protected float knockbackDuration;
    protected bool isKnocked;
    

    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFx fx { get; private set; }

    public int flipDir = 1;
    public bool facingRight = true;
    protected virtual void Awake() 
    {
    
    }

    protected virtual void Start() 
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFx>();

    }

    protected virtual void Update() 
    {
    
    
    }
    public void SetVelocity(float _xVelocity, float _yVelocity) 
    {
        if (isKnocked)
            return;
        rb.velocity = new Vector2(_xVelocity, _yVelocity);
        FlipController(_xVelocity);

    }
    public void SetZeroVelocity() 
    {
        if (isKnocked) 
            return;
        rb.velocity = new Vector2(0, 0);
    
    }

    #region Collision
    public virtual bool isGroundDetected() => Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual bool isWallDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * flipDir, wallCheckDistance, whatIsGround);
  

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion 

    #region Flip
    public virtual void Flip()
    {
        flipDir = flipDir * -1;
        facingRight = !facingRight;
        transform.Rotate(0, 180, 0);

    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
        {
            Flip();

        }
        else if (_x < 0 && facingRight)
        {
            Flip();
        }



    }
    #endregion

    public virtual void Damage() 
    {
        Debug.Log(gameObject.name + "was damaged");
        fx.StartCoroutine("FlashFx");
        StartCoroutine("HitKnockBack");

    
    }

    protected virtual IEnumerator HitKnockBack() 
    {
        isKnocked = true;
        rb.velocity = new Vector2(knockbackDirection.x * -flipDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    
    }

} 
