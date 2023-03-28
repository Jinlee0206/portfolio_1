using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_DIE : State
{
    public DragonState_DIE(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
    }

    public override void Enter()
    {

        dragon.curAnimName = "Die";
        
        dragon.agent.enabled = false;
        dragon.TriggerAnimation(dragon.hashDie);

    }

    public override void Exit()
    {

    }

    public override void PhysicsUpdate()
    {

    }

    public override void LogicUpdate()
    {

    }
}
