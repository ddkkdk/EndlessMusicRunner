using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerstateMachine stateMachine, Player player) : base(stateMachine, player)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, 10);
        //rb.velocity = new Vector2(rb.velocity.x, 17);
        // rb.AddForce(new Vector2(0, 17), ForceMode2D.Impulse);
       // player.skeletonObj.AnimationState.SetAnimation(11, "jump", true);




    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (rb.velocity.y == 0)
            stateMachine.ChangeState(player.moveState);
    }


}
