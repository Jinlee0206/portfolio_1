using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_ATTACK_RFCLAW : DragonState_ATTACK_MELEE
{
    public DragonState_ATTACK_RFCLAW(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        dragon.curAnimName = "DragonAtk_RFclaw";
        dragon.TriggerAnimation(dragon.hashDragonAtkC);

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate()
    {
    }

    public override void LogicUpdate()
    {
        RFClawAtk();
    }

    void RFClawAtk()
    {
        dragon.CheckAnimationState("DragonAtk_RFclaw");

    }
}

