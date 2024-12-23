using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_PatrolState : StateBase
{
    Monster monster;
    public Monster_PatrolState(Monster _monster)
    {
        monster = _monster;
    }
    public override void OnStateEnter()
    {
        monster.ChageTarget(monster.patrolPoint[0]);
        monster.PlayAnimation(MONSTER_STATE.MOVE);
        //Debug.Log("OnIdleEnter");
    }

    public override void OnStateExit()
    {
        //Debug.Log("OnIdleExit");
    }

    public override void OnStateUpdate()
    {
        monster.PatrolMode();

        if (monster.IsPlayerInDetectionRange())
        {
            monster.ChangeState(MONSTER_STATE.CHASE);
        }
        //Debug.Log("OnIdleUpdate");
    }
}
