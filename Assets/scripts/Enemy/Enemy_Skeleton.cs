using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton : Enemy
{
    public En_SkeletonIdleState idleState { get; private set; }
    public En_SkeletonMoveState moveState { get; private set; }

    public En_SkeletonBattleState battleState { get; private set; }

    public En_SkeletonAttackState attackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        idleState = new En_SkeletonIdleState(this, stateMachine, "Idle", this);
        moveState = new En_SkeletonMoveState(this, stateMachine, "Move", this);
        battleState = new En_SkeletonBattleState(this, stateMachine, "Move", this);
        attackState = new En_SkeletonAttackState(this, stateMachine, "Attack",this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initilization(idleState);
    }

    protected override void Update()
    {
        base.Update();
      
    }
}
