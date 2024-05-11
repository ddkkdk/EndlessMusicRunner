using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Space)) 
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;

        }
            

        if (player.isGroundDetected())
            stateMachine.ChangeState(player.idleState);

        if (xInput != 0 && player.flipDir != xInput)
            stateMachine.ChangeState(player.idleState);

        if (yInput < 0)
            _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y);
        else
            _rigidBody.velocity = new Vector2(0, _rigidBody.velocity.y * .7f);
      

    }
}
