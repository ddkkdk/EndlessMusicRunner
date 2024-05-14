using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingleAttackState : PlayerState
{
    public SingleAttackState(PlayerstateMachine stateMachine, Player player) : base(stateMachine, player)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();
      //  rb.AddForce(new Vector2(0, 17), ForceMode2D.Impulse);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
            stateMachine.ChangeState(player.moveState);
    }

}
