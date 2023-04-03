using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonState_AI : State
{
    public DragonState_AI(DragonCtrl dragon, StateMachine stateMachine) : base(dragon, stateMachine)
    {
    }

    public override void Enter()
    {
    }

    public override void Exit()
    {

    }

    public override void PhysicsUpdate()
    {

    }

    public override void LogicUpdate()
    {
        CheckDragonState();
    }

    private void CheckDragonState()
    {
        // ���ݹ��� �� && �������϶� �ƴҶ�
        if (dragon.distance <= dragon.attackDist && dragon.isMeleeState == false)
        {
            AttackPhase();
        }

        // �������� ��
        else if (dragon.distance <= dragon.traceDist && dragon.distance > dragon.attackDist && dragon.isMeleeState == false)
        {
            stateMachine.ChangeState(dragon.m_states[DragonCtrl.eState.TRACE]);
        }

        // idle(�ι�)
        // ���� �ٽ� �� �з�
        else if (dragon.distance > dragon.traceDist && dragon.isMeleeState == false)
        {
            stateMachine.ChangeState(dragon.m_states[DragonCtrl.eState.IDLE]);
        }
    }

    // ���� ��ȭ�� ������ ���� �ð��� ���� 
    void AttackPhase()
    {
        // false �϶��� ������ �������� dragon 0�� ����Ʈ�� ȣ���� �ȵǰ� or bool �� ó��
        if (dragon.isPatterning == false && dragon.patternIdx <= 4)
        {
            int rand = Random.Range(0, 5);
            dragon.pattern.DoNext(dragon, rand, 1.5f);
        }

        else if (dragon.isPatterning == false && dragon.patternIdx == 5)
        {
            dragon.pattern.DoNext(dragon, 6, 1.5f);
        }

        else if (dragon.isPatterning == false && dragon.patternIdx <= 9)
        {
            int rand = Random.Range(0, 5);
            dragon.pattern.DoNext(dragon, rand, 1.5f);
        }

        else if (dragon.isPatterning == false && dragon.patternIdx == 10)
        {
            dragon.LightOff();
            dragon.pattern.DoNext(dragon, 7, 2.5f);
        }

        else if (dragon.isPatterning == false && dragon.patternIdx <= 12)
        {
            int rand = Random.Range(0, 5);
            dragon.pattern.DoNext(dragon, rand, 1.5f);
        }

        else if (dragon.isPatterning == false && dragon.patternIdx == 13)
        {
            dragon.pattern.DoNext(dragon, 6, 1.5f);
        }

        else if (dragon.isPatterning == false && dragon.patternIdx == 14)
        {
            stateMachine.ChangeState(dragon.m_states[DragonCtrl.eState.DIE]);
        }
    }




}
