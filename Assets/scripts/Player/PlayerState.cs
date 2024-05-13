using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerstateMachine stateMachine;
    protected Player player;

    private string animBoolName;
    protected Rigidbody2D rb;

    protected bool triggerCalled;

    public PlayerState(PlayerstateMachine stateMachine, Player player, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter() 
    {
        player.animator.SetBool(animBoolName, true);
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update() 
    {
    
    }

    public virtual void Exit() 
    {
        player.animator.SetBool(animBoolName, false);

    }

    public virtual void AnimationFinishTrigger() 
    {
        triggerCalled = true;

    
    }
}
