using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Entity : MonoBehaviour
{
    [Header("Collision Info")]
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    public Transform attackCheck;
    public float attackRadius;
    public float maxHealth;
    public float currentHealth;
    public Image fillAmount;
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
    protected virtual void FixedUpdate()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOnGround = true;
        
    } 
 
    public virtual void Damage(int damageAmount) 
    {
        
        currentHealth -= damageAmount;
        SetHealthBar();
        FlashFx.Instance.callFlash();
        AudioManager.instance.PlayerHItSound();
    
    }

    public void SetHealthBar()
    {

        fillAmount.fillAmount = currentHealth / maxHealth;


    }
}
