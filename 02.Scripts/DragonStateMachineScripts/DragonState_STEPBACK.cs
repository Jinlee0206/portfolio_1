using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_STEPBACK : State
{
    private float jumpForce = 10f;

    public DragonState_STEPBACK(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
    }

    public override void Enter()
    {
        dragon.agent.enabled = false;
        dragon.curAnimName = "StepBack";

        dragon.StartCoroutine(Teleportation());
    }

    public override void Exit()
    {
        dragon.agent.enabled = true;
    }

    public override void PhysicsUpdate()
    {
        StepBack();
    }

    public override void LogicUpdate()
    {
        dragon.CheckAnimationState("StepBack");
    }

    IEnumerator Teleportation()
    {
        WaitUntil delay = new WaitUntil(() => dragon.animName && dragon.animTime >= 0.9f);

        float rtnTime = 5f;
        ObjectPoolingManager.instance.GetObject("Teleportation", dragon.transform.position, dragon.transform.rotation, rtnTime);
        ObjectPoolingManager.instance.GetObject("DarkDimension", dragon.transform.position, dragon.transform.rotation, rtnTime);

        yield return delay;
        dragon.isMeleeState = false;
    }

    void StepBack()
    {
        dragon.transform.position = Vector3.Lerp(dragon.transform.position, dragon.stepBackPos.transform.position, 0.9f);
        dragon.TriggerAnimation(dragon.hashStepBack);
    }


}
