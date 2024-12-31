using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_DieState : StateBase
{
    Monster monster;
    public Monster_DieState(Monster _monster)
    {
        monster = _monster;
    }
    public override void OnStateEnter()
    {
        monster.PlayAnimation("Ani_State", (int)MONSTER_ANI_STATE.DIE);
        //Debug.Log("OnMoveEnter");
    }

    public override void OnStateExit()
    {
        //Debug.Log("OnMoveExit");
    }

    public override void OnStateUpdate()
    {
        //Debug.Log("OnMoveUpdate");
    }
}
