using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_TRACE : State
{
    
    public DragonState_TRACE(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
    }

    public override void Enter()
    {
        dragon.agent.isStopped = false;
        dragon.agent.SetDestination(dragon.playerTr.position);
        dragon.TriggerAnimation(dragon.hashTrace);
    }

    public override void Exit()
    {
        dragon.agent.isStopped = true;
    }

    public override void PhysicsUpdate()
    {
        RotateDragon();
    }

    public override void LogicUpdate()
    {
        if (dragon.distance > dragon.traceDist && dragon.isMeleeState == false)
        {
            stateMachine.ChangeState(dragon.m_states[DragonCtrl.eState.AI]);
        }
        if (dragon.distance < dragon.attackDist && dragon.isMeleeState == false)
        {
            stateMachine.ChangeState(dragon.m_states[DragonCtrl.eState.AI]); 
        }

    }

    void RotateDragon()
    {
        Vector3 dir = dragon.playerTr.position - dragon.transform.position;

        dragon.dragonTr.transform.localRotation = Quaternion.Slerp(dragon.dragonTr.transform.localRotation, Quaternion.LookRotation(dir), 10 * Time.deltaTime);
    }
}
