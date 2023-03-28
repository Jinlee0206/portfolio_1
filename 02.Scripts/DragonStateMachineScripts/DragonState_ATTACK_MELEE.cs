using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_ATTACK_MELEE : State
{
    public DragonState_ATTACK_MELEE(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
    }

    public override void Enter()
    {
        dragon.isMeleeState = true;
        dragon.agent.isStopped = true;
        dragon.agent.velocity = Vector3.zero;
        dragon.agent.ResetPath();
    }

    public override void Exit()
    {
        dragon.isMeleeState = false;
    }

    public override void PhysicsUpdate()
    {
    }

    public override void LogicUpdate()
    {
        
    }
}
