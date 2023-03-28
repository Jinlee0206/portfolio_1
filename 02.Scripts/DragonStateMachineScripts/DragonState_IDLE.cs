using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_IDLE : State
{



    public DragonState_IDLE(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
    }

    public override void Enter()
    {
        dragon.agent.isStopped = true;
        dragon.anim.Rebind();
    }

    public override void Exit()
    {

    }

    public override void PhysicsUpdate()
    {
        
    }

    public override void LogicUpdate()
    {
        CheckStance();
    }

    private void CheckStance()
    {
        if (dragon.distance > dragon.traceDist)
        {
            return;
        }
        else
        {
            Debug.Log("AI");
            stateMachine.ChangeState(dragon.m_states[DragonCtrl.eState.AI]);
        }
    }


}
