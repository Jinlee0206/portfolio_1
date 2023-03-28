using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_ATTACK_BITE : DragonState_ATTACK_MELEE
{
    public DragonState_ATTACK_BITE(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        dragon.curAnimName = "DragonAtk_Jbite";
        dragon.TriggerAnimation(dragon.hashDragonAtkA);
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
        BiteAtk();
    }

    void BiteAtk()
    {
        dragon.CheckAnimationState("DragonAtk_Jbite");
    }

}
