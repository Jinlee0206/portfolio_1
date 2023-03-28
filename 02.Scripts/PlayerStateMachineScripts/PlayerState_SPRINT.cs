using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_SPRINT : PlayerState_GROUNDED
{
    private bool sprintHeld;

    public PlayerState_SPRINT(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetAnimationBool(player.hashSprintParam, true);
        speed = player.pRunSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetAnimationBool(player.hashSprintParam, false);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        sprintHeld = Input.GetButton("Sprint");

        if (!(sprintHeld))
        {
            stateMachine.ChangeState(player.m_pStates[PlayerCtrl.eState.STANDING]);
        }
    }
}