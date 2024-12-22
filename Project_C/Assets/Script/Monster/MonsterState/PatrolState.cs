using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : StateBase
{
    Monster monster;
    public PatrolState(Monster _monster)
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
        //Debug.Log("OnIdleUpdate");
    }
}
