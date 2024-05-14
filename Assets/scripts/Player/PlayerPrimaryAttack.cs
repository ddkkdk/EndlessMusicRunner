using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;

    private float lastTimeAttack;
    private float comboWindow = 2;
    public PlayerPrimaryAttack(PlayerstateMachine stateMachine, Player player) : base(stateMachine, player)
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


    }
}
