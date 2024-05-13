using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroundState : PlayerState
{
    public PlayerGroundState(PlayerstateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
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
        if (Input.GetKeyDown(KeyCode.F))
            stateMachine.ChangeState(player.jumpState);

        if(Input.GetKeyDown(KeyCode.J))
            stateMachine.ChangeState(player.primaryAttack);
    }
}
