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
        monster.PatrolModeInit();
        monster.PlayAnimation("Ani_State",(int)MONSTER_ANI_STATE.MOVE);
    }

    public override void OnStateExit()
    {
        monster.SetPatrolIndex(0);
        //Debug.Log("OnIdleExit");
    }

    public override void OnStateUpdate()
    {
        monster.PatrolMode();

        if (monster.IsPlayerInDetectionRange())
        {
            monster.ChageTarget(Shared.GameMgr.PLAYER);
            monster.ChangeState(MONSTER_STATE.CHASE);
        }
        //Debug.Log("OnIdleUpdate");
    }
}
