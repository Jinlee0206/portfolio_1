using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_LAND : State
{
    private float flyHeight = 25f;


    public DragonState_LAND(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
        
    }

    public override void Enter()
    {

        dragon.isMeleeState = true;
        dragon.TriggerAnimation(dragon.hashisLand);
    }

    public override void Exit()
    {
        dragon.isMeleeState = false;
        dragon.isFlying = false;
        dragon.agent.enabled = true;
    }

    public override void PhysicsUpdate()
    {
        if (dragon.transform.position.y > 20)
        {
            dragon.transform.position -= Vector3.up * (flyHeight * 0.1f) * Time.deltaTime;
        }

        if (dragon.transform.position.y <= 20 + float.Epsilon)
        {
            stateMachine.ChangeState(dragon.m_states[DragonCtrl.eState.AI]);
        }


    }

    public override void LogicUpdate()
    {
        
    }

}
