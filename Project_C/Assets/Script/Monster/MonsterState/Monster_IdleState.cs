using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_IdleState : StateBase
{
    Monster monster;
    public Monster_IdleState(Monster _monster)
    {
        monster = _monster;
    }

    public override void OnStateEnter()
    {
        monster.PlayAnimation("Ani_State", (int)MONSTER_ANI_STATE.IDLE);
        if(monster.OnPatrol)
        {
            monster.StartChageToPatrol(2.0f);
        }
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
