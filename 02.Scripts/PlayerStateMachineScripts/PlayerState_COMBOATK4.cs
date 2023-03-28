using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_COMBOATK4 : pState
{
    public PlayerState_COMBOATK4(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.curAnimName = "ComboAtk4";
        base.Enter();
        Attack_4(player);
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
        player.ColliderActive(player, "ComboAtk4");
        player.ComboAtkCheck(PlayerCtrl.eState.COMBOATK1, "ComboAtk4");

    }

    void Attack_4(PlayerCtrl player)
    {
        player.TriggerAnimation(player.hashComboAtk4);
    }
}