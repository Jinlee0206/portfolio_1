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
        // 공격범위 안 && 공격중일때 아닐때
        if (dragon.distance <= dragon.attackDist && dragon.isMeleeState == false)
        {
            AttackPhase();
        }

        // 추적범위 안
        else if (dragon.distance <= dragon.traceDist && dragon.distance > dragon.attackDist && dragon.isMeleeState == false)
        {
            stateMachine.ChangeState(dragon.m_states[DragonCtrl.eState.TRACE]);
        }

        // idle(로밍)
        // 조건 다시 상세 분류
        else if (dragon.distance > dragon.traceDist && dragon.isMeleeState == false)
        {
            stateMachine.ChangeState(dragon.m_states[DragonCtrl.eState.IDLE]);
        }
    }

    // 영상 녹화에 초점을 맞춰 시간에 따라 
    void AttackPhase()
    {
        // false 일때만 들어오고 나머지는 dragon 0번 리스트가 호출이 안되게 or bool 값 처리
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
