using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_STANDING : PlayerState_GROUNDED
{
    private bool crouch;
    private bool sprint;
    private bool comboAtk;
    private bool skill1;
    private bool skill2;
    private bool jumpAtk;
    private bool rollback;
    private bool down;

    public PlayerState_STANDING(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        speed = player.pWalkSpeed;

        crouch = false;
        sprint = false;
        comboAtk = false;
        skill1 = false;
        skill2 = false;
        jumpAtk = false;
        rollback = false;
        down = false;

    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();

        crouch = Input.GetButtonDown("Crouch");
        sprint = Input.GetButtonDown("Sprint");
        comboAtk = Input.GetMouseButtonDown(0);
        skill1 = Input.GetKeyDown(KeyCode.Alpha1);
        skill2 = Input.GetKeyDown(KeyCode.Alpha2);
        jumpAtk = Input.GetMouseButtonDown(1);
        rollback = Input.GetButtonDown("Jump");
        down = Input.GetButtonDown("Down");
        

        if (crouch)
        {
            stateMachine.ChangeState(player.m_pStates[PlayerCtrl.eState.CROUCH]);
        }

        if (sprint)
        {
            stateMachine.ChangeState(player.m_pStates[PlayerCtrl.eState.SPRINT]);
        }

        if (comboAtk)
        {
            stateMachine.ChangeState(player.m_pStates[PlayerCtrl.eState.COMBOATK1]);
        }

        if (skill1)
        {
            stateMachine.ChangeState(player.m_pStates[PlayerCtrl.eState.SKILL1]);
        }

        if (skill2)
        {
            stateMachine.ChangeState(player.m_pStates[PlayerCtrl.eState.SKILL2]);
        }

        if (jumpAtk)
        {
            stateMachine.ChangeState(player.m_pStates[PlayerCtrl.eState.JUMPATK]);
        }

        if (rollback)
        {
            stateMachine.ChangeState(player.m_pStates[PlayerCtrl.eState.ROLL]);
        }

        if (down)
        {
            stateMachine.ChangeState(player.m_pStates[PlayerCtrl.eState.DOWN]);
        }

    }


}
