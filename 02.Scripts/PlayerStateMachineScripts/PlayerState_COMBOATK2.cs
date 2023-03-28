using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_COMBOATK2 : pState
{
    public PlayerState_COMBOATK2(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.curAnimName = "ComboAtk2";
        base.Enter();
        Attack_2(player);
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
        player.ColliderActive(player, "ComboAtk2");
        player.ComboAtkCheck(PlayerCtrl.eState.COMBOATK3, "ComboAtk2");

    }

    void Attack_2(PlayerCtrl player)
    {
        player.TriggerAnimation(player.hashComboAtk2);
    }
}