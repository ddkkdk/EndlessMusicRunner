using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_SkeletonIdleState : En_SkeletonGroundState
{
    public En_SkeletonIdleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        startTimer = enemy.idleTime;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (startTimer < 0)
            statemachine.ChangeState(enemy.moveState);
    }
}
 