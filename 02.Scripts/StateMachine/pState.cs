using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class pState
{
    protected PlayerCtrl player;
    protected pStateMachine stateMachine;

    protected pState(PlayerCtrl player, pStateMachine stateMachine)
    {
        this.player = player;
        this.stateMachine = stateMachine;
    }
    public virtual void Enter()
    {

    }

    public virtual void Exit()
    {
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void LogicUpdate()
    {

    }


}