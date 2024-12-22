using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : StateBase
{
    Monster monster;
    public AttackState(Monster _monster)
    {
        monster = _monster;
    }
    public override void OnStateEnter()
    {
        monster.PlayAnimation(MONSTER_STATE.ATTACK);
        //Debug.Log("OnAttackEnter");
    }

    public override void OnStateExit()
    {
        //Debug.Log("OnAttackExit");
    }

    public override void OnStateUpdate()
    {
        if (monster.IsPlayerInAttackRange() == false)
            monster.ChageState(MONSTER_STATE.MOVE);
       // Debug.Log("OnAttackUpdate");
    }
}
