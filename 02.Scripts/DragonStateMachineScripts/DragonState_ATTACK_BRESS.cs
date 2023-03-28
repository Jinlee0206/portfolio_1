using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_ATTACK_BRESS : DragonState_ATTACK_MELEE
{
    public DragonState_ATTACK_BRESS(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
    }

    public override void Enter()
    {
        base.Enter();
        dragon.curAnimName = "DragonAtk_Bress";
        dragon.StartCoroutine(BressAtk(dragon));
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void PhysicsUpdate()
    {

    }

    public override void LogicUpdate()
    {
        Bress();
    }

    void Bress()
    {
        dragon.CheckAnimationState("DragonAtk_Bress");
    }

    IEnumerator BressAtk(DragonCtrl dragon)
    {
        Debug.Log("Bress준비");
        dragon.TriggerAnimation(dragon.hashSniffleAround);
        yield return new WaitForSeconds(3.0f);

        dragon.TriggerAnimation(dragon.hashBress);

        WaitUntil delay = new WaitUntil(() => dragon.animName && dragon.animTime >= 0.2f);

        float returnTime = 3f;
        float arrowrtnTime = 1.5f;

        yield return delay;

        ObjectPoolingManager.instance.GetObject("Bress", dragon.bressPos.position, dragon.bressPos.rotation, returnTime);
        ObjectPoolingManager.instance.GetObject("MultipleArrow", dragon.bressPos.position, dragon.bressPos.rotation, arrowrtnTime);
        ObjectPoolingManager.instance.GetObject("SpearAttack", dragon.bressPos.position, dragon.bressPos.rotation, returnTime);

        Debug.Log("Bress발사");

    }

}