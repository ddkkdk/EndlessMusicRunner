using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboTime = 2;
    public PlayerAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        xInput = 0;
        if (comboCounter > 2 || Time.time>=lastTimeAttacked+comboTime)
            comboCounter = 0;

        player.anim.SetInteger("ComboCounter", comboCounter);

        float attckdir = player.flipDir;

        if(xInput!=0)
            attckdir = xInput;

        player.SetVelocity(player.attackMovement[comboCounter].x * attckdir, player.attackMovement[comboCounter].y);

        stateTimer = 0.1f;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", 0.1f); 

        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);

        if (stateTimer < 0)
            _rigidBody.velocity = new Vector2(0, 0);
    }
}

