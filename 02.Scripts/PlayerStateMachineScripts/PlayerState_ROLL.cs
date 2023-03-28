using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_ROLL : pState
{

    public PlayerState_ROLL(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.curAnimName = "RollBack";
        player.StartCoroutine(JumpDelay());
    }

    public override void Exit()
    {
    }

    public override void PhysicsUpdate()
    {
    }

    public override void LogicUpdate()
    {
        base.LogicUpdate();
        RollBack();
    }

    private void RollBack()
    {
        player.CheckAnimationState("RollBack");
    }

    IEnumerator JumpDelay()
    {
        player.TriggerAnimation(player.hashRollBack);

        float delayTime = 0.35f;
        yield return new WaitForSeconds(delayTime);
        player.characterController.Move(player.transform.forward * -7);

    }

}