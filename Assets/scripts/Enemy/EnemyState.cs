using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    protected EnemyStateMachine statemachine { get; private set; }
    protected Enemy enemyBase { get; private set; }
    protected string animBoolName { get; private set; }

    protected float startTimer;
    protected bool triggerCalled;

    protected Rigidbody2D enemyRb;

    public EnemyState(Enemy _enemyBase, EnemyStateMachine _stateMachine,string _animBoolName) 
    {
        this.enemyBase = _enemyBase;
        this.statemachine = _stateMachine;
        this.animBoolName = _animBoolName; 
    
    }

    public virtual void Enter() 
    {
        triggerCalled = false;
        enemyRb = enemyBase.rb;
        enemyBase.anim.SetBool(animBoolName, true);
    }

    public virtual void Exit() 
    {
        enemyBase.anim.SetBool(animBoolName, false);

    }

    public virtual void Update() 
    {
       startTimer-= Time.deltaTime;
    
    }

    public virtual void AnimationFinishTrigger()
    {
        triggerCalled = true;

    }

}
