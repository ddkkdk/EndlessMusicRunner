using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(PlayerstateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
       // rb.velocity = new Vector2(rb.velocity.x, 17);
        //rb.velocity = new Vector2(rb.velocity.x, 17);
        rb.AddForce(new Vector2(0, 17), ForceMode2D.Impulse);
        stateMachine.ChangeState(player.singleAttack);



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
