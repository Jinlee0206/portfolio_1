using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_ATTACK_SPITFIRE : State
{


    public DragonState_ATTACK_SPITFIRE(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
    }

    public override void Enter()
    {
        dragon.curAnimName = "DragonAtk_SpitFire";

        dragon.isMeleeState = true;

        dragon.StartCoroutine(Delay(dragon));
    }

    public override void Exit()
    {
        dragon.isMeleeState = false;
    }

    public override void PhysicsUpdate()
    {

    }

    public override void LogicUpdate()
    {
        SpitFire();
    }


    void SpitFire()
    {
        dragon.CheckAnimationState("DragonAtk_SpitFire");
    }

    IEnumerator Delay(DragonCtrl dragon)
    {
        Debug.Log("PulseShot준비");
        dragon.TriggerAnimation(dragon.hashDragonAtkSpitFire);

        WaitUntil delay = new WaitUntil(() => dragon.animName && dragon.animTime >= 0.35f);

        float returnTime = 3f;

        yield return delay;

        ObjectPoolingManager.instance.GetObject("PulseShot", dragon.pulsePos.position, dragon.pulsePos.rotation, returnTime);

        Debug.Log("PulseShot발사");

    }



}

