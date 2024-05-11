using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_SkeletonMoveState : En_SkeletonGroundState
{
    public En_SkeletonMoveState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
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

        enemy.SetVelocity(enemy.moveSpeed * enemy.flipDir, enemyRb.velocity.y);

        if (enemy.isWallDetected() || !enemy.isGroundDetected()) 
        {
            enemy.Flip();
            statemachine.ChangeState(enemy.idleState);
        
        }
    }
}

