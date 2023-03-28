using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_FLY : State
{
    private float flyHeight = 25f;


    public DragonState_FLY(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
    }

    public override void Enter()
    {
        dragon.isFlying = true;

        // DragonMovePattern �� Do pattern Ŀ�ǵ��� while���� �����Ű�� ����
        dragon.isMeleeState = true;

        dragon.agent.enabled = false;
        dragon.TriggerAnimation(dragon.hashisFlyB);

        // 10�ʵڿ� �ڷ�ƾ ���� �� MeleeState�� On
        dragon.StartCoroutine(delay());

    }

    public override void Exit()
    {
        dragon.isFlying = false;
    }

    public override void PhysicsUpdate()
    {
        if (dragon.transform.position.y < flyHeight)
        {
            dragon.transform.position += Vector3.up * (flyHeight * 0.1f) * Time.deltaTime;
        }
    }

    public override void LogicUpdate()
    {
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(2.5f);
        dragon.isMeleeState = false;
    }
}
