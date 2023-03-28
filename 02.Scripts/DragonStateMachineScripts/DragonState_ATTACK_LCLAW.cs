using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_ATTACK_LCLAW : DragonState_ATTACK_MELEE
{

    public DragonState_ATTACK_LCLAW(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        dragon.curAnimName = "DragonAtk_Lclaw";

        dragon.TriggerAnimation(dragon.hashDragonAtkB);
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
        LClawAtk();
    }

    void LClawAtk()
    {
        dragon.CheckAnimationState("DragonAtk_Lclaw");
    }


}
