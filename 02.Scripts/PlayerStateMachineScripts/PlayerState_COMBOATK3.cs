using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_COMBOATK3 : pState
{
    public PlayerState_COMBOATK3(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.curAnimName = "ComboAtk3";
        base.Enter();
        Attack_3(player);
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
        player.ColliderActive(player, "ComboAtk3");
        player.ComboAtkCheck(PlayerCtrl.eState.COMBOATK4, "ComboAtk3");

    }

    void Attack_3(PlayerCtrl player)
    {
        player.TriggerAnimation(player.hashComboAtk3);
    }
}