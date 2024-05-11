using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class En_SkeletonBattleState : EnemyState
{
    private Enemy_Skeleton enemy;
    private Transform player;
    private int moveDir;
    public En_SkeletonBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
        {
            startTimer = enemy.battleTime;

            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                if (CanAttack())
                    statemachine.ChangeState(enemy.attackState);

            }

        }
        else 
        {
            if(startTimer<0 || Vector2.Distance(player.transform.position,enemy.transform.position)>15)
                statemachine.ChangeState(enemy.idleState );
        
        }

        if(player.transform.position.x >enemy.transform.position.x)
            moveDir = 1;
        else if(player.transform.position.x<enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, enemyRb.velocity.y); 

    }

    private bool CanAttack() 
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCoolDown) 
        {
            enemy.lastTimeAttacked=Time.time;
            return true;
        
        }
        return false;
    
    }
}
