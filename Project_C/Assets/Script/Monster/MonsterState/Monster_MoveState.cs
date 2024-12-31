using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_MoveState : StateBase
{
    Monster monster;
    public Monster_MoveState(Monster _monster)
    {
        monster = _monster;
    }
    public override void OnStateEnter()
    {
        monster.PlayAnimation("Ani_State", (int)MONSTER_ANI_STATE.MOVE);
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
            monster.ChangeState(MONSTER_STATE.ATTACK);

        //Debug.Log("OnMoveUpdate");
    }
}
