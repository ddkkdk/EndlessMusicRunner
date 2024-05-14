using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerstateMachine stateMachine;
    protected Player player;

    
    protected Rigidbody2D rb;

    protected bool triggerCalled;

    public PlayerState(PlayerstateMachine stateMachine, Player player)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        
    }

    public virtual void Enter() 
    {
      
        rb = player.rb;
        triggerCalled = false;
    }

    public virtual void Update() 
    {
    
    }

    public virtual void Exit() 
    {
        // player.animator.SetBool(animBoolName, false);
       // player.skeletonObj.AnimationState.SetAnimation(16, "Running", false);

    }

    public virtual void AnimationFinishTrigger() 
    {
        triggerCalled = true;

    
    }
}
