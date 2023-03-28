using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_JUMPATK : pState
{
    float jumpHeight = 5f;
    float gravity = -9.81f;

    public PlayerState_JUMPATK(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.curAnimName = "JumpAtk";
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
        player.ColliderActive(player, "JumpAtk");
        JumpAtk();
    }

    private void JumpAtk()
    {
        player.CheckAnimationState("JumpAtk");
    }

    IEnumerator JumpDelay()
    {
        player.TriggerAnimation(player.hashJumpAtk);

        float delayTime = 0.5f;
        yield return new WaitForSeconds(delayTime);
        player.characterController.Move(player.transform.forward * 5);

    }

}