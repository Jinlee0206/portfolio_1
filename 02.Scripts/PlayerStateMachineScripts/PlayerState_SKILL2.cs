using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_SKILL2 : pState
{
    public PlayerState_SKILL2(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.curAnimName = "Skill2";
        player.StartCoroutine(Delay(player));
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
        CastFireball();
    }

    private void CastFireball()
    {
        player.CheckAnimationState("Skill2");
    }

    IEnumerator Delay(PlayerCtrl player)
    {
        player.TriggerAnimation(player.hashSkill2);

        WaitUntil delay = new WaitUntil(() => player.animName && player.animTime >= 0.2f);
        WaitUntil delay2 = new WaitUntil(() => player.animName && player.animTime >= 0.24f);
        WaitUntil delay3 = new WaitUntil(() => player.animName && player.animTime >= 0.65f);

        float returnTime = 2f;

        GameObject FireField = ObjectPoolingManager.instance.GetObject(
            "FireField", player.transform.position, player.transform.rotation);

        yield return delay;

        ObjectPoolingManager.instance.GetObject(
            "FastestFist", player.transform.position, player.transform.rotation, returnTime);

        yield return delay2;

        ObjectPoolingManager.instance.GetObject(
            "FastestFist", player.transform.position, player.transform.rotation, returnTime);

        yield return delay3;

        ObjectPoolingManager.instance.GetObject(
            "Smash", player.transform.position, player.transform.rotation, returnTime);

        ObjectPoolingManager.instance.ReturnObject(FireField);
    }
}
