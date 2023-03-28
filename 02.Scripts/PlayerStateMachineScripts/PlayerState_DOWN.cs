using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_DOWN : pState
{
    public PlayerState_DOWN(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.curAnimName = "Down";
        player.TriggerAnimation(player.hashDown);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        player.CheckAnimationState("Down");
    }


}