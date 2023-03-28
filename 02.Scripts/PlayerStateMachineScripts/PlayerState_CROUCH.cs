using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_CROUCH : PlayerState_GROUNDED
{
    private bool crouchHeld;

    public PlayerState_CROUCH(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetAnimationBool(player.hashCrouchParam, true);
        speed = player.pCrouchSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        player.SetAnimationBool(player.hashCrouchParam, false);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        crouchHeld = Input.GetButton("Crouch");

        if (!(crouchHeld))
        {
            stateMachine.ChangeState(player.m_pStates[PlayerCtrl.eState.STANDING]);
        }
    }
}
