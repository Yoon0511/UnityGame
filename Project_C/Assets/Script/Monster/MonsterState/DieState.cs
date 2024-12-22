using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DieState : StateBase
{
    Monster monster;
    public DieState(Monster _monster)
    {
        monster = _monster;
    }
    public override void OnStateEnter()
    {
        monster.PlayAnimation(MONSTER_STATE.DIE);
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
