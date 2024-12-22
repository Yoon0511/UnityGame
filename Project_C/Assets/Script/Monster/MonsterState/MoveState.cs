using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveState : StateBase
{
    Monster monster;
    public MoveState(Monster _monster)
    {
        monster = _monster;
    }
    public override void OnStateEnter()
    {
        monster.PlayAnimation(MONSTER_STATE.MOVE);
        //Debug.Log("OnMoveEnter");
    }

    public override void OnStateExit()
    {
        //Debug.Log("OnMoveExit");
    }

    public override void OnStateUpdate()
    {
        monster.MoveToTarget();

        if (monster.IsPlayerInAttackRange())
            monster.ChageState(MONSTER_STATE.ATTACK);

        //Debug.Log("OnMoveUpdate");
    }
}
