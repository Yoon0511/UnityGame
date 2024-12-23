using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_ChaseState : StateBase
{
    Monster monster;
    public Monster_ChaseState(Monster _monster)
    {
        monster = _monster;
    }
    public override void OnStateEnter()
    {
        monster.PlayAnimation(MONSTER_STATE.MOVE);
        //Debug.Log("OnIdleEnter");
    }

    public override void OnStateExit()
    {
        //Debug.Log("OnIdleExit");
    }

    public override void OnStateUpdate()
    {
        monster.MoveToTarget(Shared.GameMgr.PLAYER);

        if (monster.IsPlayerInAttackRange())
            monster.ChangeState(MONSTER_STATE.ATTACK);
        //Debug.Log("OnIdleUpdate");
    }
}
