using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_AttackState : StateBase
{
    Monster monster;
    public Monster_AttackState(Monster _monster)
    {
        monster = _monster;
    }
    public override void OnStateEnter()
    {
        monster.PlayAnimation("Ani_State", (int)MONSTER_ANI_STATE.ATTACK);
    }

    public override void OnStateExit()
    {
        //Debug.Log("OnAttackExit");
    }

    public override void OnStateUpdate()
    {
        if (monster.IsPlayerInAttackRange() == false) //플레이거가 공격사거리 밖으로 벗어날 경우
            monster.ChangeState((int)MONSTER_STATE.CHASE);
    }
}
