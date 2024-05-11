using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;

    [Header("Attack Info")]
    public float attackDistance;
    public float attackCoolDown;

    [HideInInspector] public float lastTimeAttacked;

   public EnemyStateMachine stateMachine { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine=new EnemyStateMachine();
    }

    protected override void Update()
    {
        base.Update(); 
        stateMachine.currentState.Update();
        
    }

    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * flipDir, 50, whatIsPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * flipDir, transform.position.y));
    }

    public virtual void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
}
