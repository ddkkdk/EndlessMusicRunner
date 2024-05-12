 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : Entity
{
    [Header("Attack details")]
    public Vector2[] attackMovement;

    [Header("MoveInfo")]
    public float moveSpeed;
    public float jumpSpeed;

    [Header("Dash Info")]
    [SerializeField] private float dashCoolDown;
    private float dashTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDirection { get; private set; }

    public bool isBusy { get; private set; }

 
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }

    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlideState { get; private set; }
    public PlayerAttackState attackState { get; private set; }  
    public PlayerWallJumpState wallJumpState { get; private set; }

    
    protected override void Awake()
    {
        base.Awake();
        stateMachine=new PlayerStateMachine();
        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new  PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlideState = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        attackState = new PlayerAttackState(this, stateMachine, "Attack");
        wallJumpState = new PlayerWallJumpState(this, stateMachine, "WallJump");
    }

    protected override void Start()
    {
        base.Start();
    
        stateMachine.Initialized(moveState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        CHeckForDashInput();      
       
    }

    public IEnumerator BusyFor(float _seconds) 
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;
    
    }

    private void CHeckForDashInput() 
    {
        dashTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashTimer<0) 
        {
            dashTimer = dashCoolDown;
            dashDirection = Input.GetAxisRaw("Horizontal");

            if (dashDirection == 0)
                dashDirection = flipDir;

            stateMachine.ChangeState(dashState);
        }
         
    
    }

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

  

}
