using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;

    private float lastTimeAttack;
    private float comboWindow = 2;
    public PlayerPrimaryAttack(PlayerstateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (comboCounter > 2 || Time.time >=lastTimeAttack+comboWindow)
            comboCounter = 0;

        player.animator.SetInteger("ComboCounter", comboCounter);
    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        lastTimeAttack = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
            stateMachine.ChangeState(player.moveState);
    }
}
