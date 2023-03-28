using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonMovePattern : MonoBehaviour
{
    public string[] levels;

    private List<string> ParseCommands(string str)
    {
        List<string> list = new List<string>();
        string[] splits = str.Split(',');
        foreach (var split in splits)
            list.Add(split);
        return list;
    }

    public void DoNext(DragonCtrl dragon, int id, float cooltime)
    {
        if (levels.Length == 0)
            return;

        List<string> commands = ParseCommands(levels[id]);
        StartCoroutine(DoCommands(dragon, commands, cooltime));
        dragon.patternIdx++;
        Debug.Log("patterIdx : " + dragon.patternIdx);
    }

    IEnumerator DoCommands(DragonCtrl dragon, List<string> commands, float cooltime)
    {
        int idx = 0;

        while (idx <= commands.Count - 1)
        {
            dragon.isPatterning = true;
            ChangeState(dragon, commands[idx++]);
            yield return new WaitUntil(() => dragon.isMeleeState == false);
        }
        yield return StartCoroutine(CoolTime(dragon, cooltime));
        dragon.isPatterning = false;

    }

    IEnumerator CoolTime(DragonCtrl dragon,float cooltime)
    {
        while (cooltime > 1f)
        {
            //Debug.Log("Time: " + cooltime);
            cooltime -= Time.deltaTime;
            yield return null;
        }
    }

    void ChangeState(DragonCtrl dragon, string command)
    {
        switch (command)
        {
            case "BITE":
                dragon.m_dragonSM.ChangeState(dragon.m_states[DragonCtrl.eState.ATTACK_BITE]);
                break;
            case "LCLAW":
                dragon.m_dragonSM.ChangeState(dragon.m_states[DragonCtrl.eState.ATTACK_LCLAW]);
                break;
            case "RFCLAW":
                dragon.m_dragonSM.ChangeState(dragon.m_states[DragonCtrl.eState.ATTACK_RFCLAW]);
                break;
            case "FLY":
                dragon.m_dragonSM.ChangeState(dragon.m_states[DragonCtrl.eState.FLY]);
                break;
            case "SPITFIRE":
                dragon.m_dragonSM.ChangeState(dragon.m_states[DragonCtrl.eState.ATTACK_SPITFIRE]);
                break;
            case "LAND":
                dragon.m_dragonSM.ChangeState(dragon.m_states[DragonCtrl.eState.LAND]);
                break;
            case "STEPBACK":
                dragon.m_dragonSM.ChangeState(dragon.m_states[DragonCtrl.eState.STEPBACK]);
                break;
            case "BRESS":
                dragon.m_dragonSM.ChangeState(dragon.m_states[DragonCtrl.eState.ATTACK_BRESS]);
                break;

        }
    }


}
