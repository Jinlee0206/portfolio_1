using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected DragonCtrl dragon;
    protected StateMachine stateMachine;

    protected State(DragonCtrl dragon, StateMachine stateMachine)
    {
        this.dragon = dragon;
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
