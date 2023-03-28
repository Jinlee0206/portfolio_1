using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState_SKILL1 : pState
{
    public PlayerState_SKILL1(PlayerCtrl player, pStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Enter()
    {
        player.curAnimName = "Skill1";
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
        CastLightning();
    }

    private void CastLightning()
    {
        player.CheckAnimationState("Skill1");
    }

    IEnumerator Delay(PlayerCtrl player)
    {
        Debug.Log("Lightning 준비");
        player.TriggerAnimation(player.hashSkill1);

        WaitUntil delay = new WaitUntil(() => player.animName && player.animTime >= 0.15f);
        WaitUntil delay2 = new WaitUntil(() => player.animName && player.animTime >= 0.35f);

        float returnTime = 3f;


        ObjectPoolingManager.instance.GetObject("LightningField", player.transform.position, player.transform.rotation, returnTime);

        yield return delay;

        ObjectPoolingManager.instance.GetObject("LightningStrike_2", player.transform.position, player.transform.rotation, returnTime);
        ObjectPoolingManager.instance.GetObject("PowerOfLightning", player.transform.position, player.transform.rotation, returnTime);

        yield return delay2;
        ObjectPoolingManager.instance.GetObject("LightningStrike", player.transform.position, player.transform.rotation, returnTime);

        Debug.Log("Lightning 발사");

    }
}
