using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public SkeletonAnimation skeletonObj;
    public PlayerstateMachine stateMachine { get; private set; }

    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; set; }
    //public PlayerPrimaryAttack primaryAttack { get; private set; }

   // public SingleAttackState singleAttack { get; private set; }

  //  public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }


    private void Awake()
    {
        stateMachine = new PlayerstateMachine();
        moveState = new PlayerMoveState( stateMachine, this);
        jumpState = new PlayerJumpState(stateMachine, this);
        //primaryAttack = new PlayerPrimaryAttack(stateMachine, this, "Attack");
        //singleAttack = new SingleAttackState(stateMachine, this, "SingleAttack");*/
    }

    private void Start()
    {
       // animator = GetComponentInChildren<Animator>();  
        skeletonObj=GetComponentInChildren<SkeletonAnimation>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.Initialize(moveState);
    }

    private void Update()
    {
       stateMachine.currentState.Update();
    }

    public void SetVelocity(float _xVelocity, float _yVelocity) 
    {
        rb.velocity = new Vector2(_xVelocity, _yVelocity);

    
    }

  //  public void AnimationTrigger()=>stateMachine.currentState.AnimationFinishTrigger();

}
