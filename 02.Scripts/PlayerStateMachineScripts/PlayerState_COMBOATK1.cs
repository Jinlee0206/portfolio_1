using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_COMBOATK1 : pState
{
    public PlayerState_COMBOATK1(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.curAnimName = "ComboAtk1";
        base.Enter();
        Attack_1(player);
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
        player.ColliderActive(player, "ComboAtk1");
        player.ComboAtkCheck(PlayerCtrl.eState.COMBOATK2, "ComboAtk1");
    }

    void Attack_1(PlayerCtrl player)
    {
        player.TriggerAnimation(player.hashComboAtk1);
    }
}
