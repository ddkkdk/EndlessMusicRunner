using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Collision Info")]
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    public Transform attackCheck;
    public float attackRadius;


    protected  bool isOnGround = true;

    
    protected virtual void Awake() 
    {
    
    }
    protected virtual void Start()
    {
        
        
    }

    
    protected virtual void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
        Debug.Log(isOnGround);
    } 

    

    public virtual void Damage() 
    {
        Debug.Log(gameObject.name + "was damaged");
        FlashFx.Instance.callFlash();

        
    
    }
}
