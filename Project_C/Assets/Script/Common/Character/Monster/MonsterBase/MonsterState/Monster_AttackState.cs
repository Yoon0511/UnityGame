using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_AttackState : StateBase
{
    Monster monster;
    float ElapsedTime;
    float StateChangeTime;
    public Monster_AttackState(Monster _monster)
    {
        monster = _monster;
    }
    public override void OnStateEnter()
    {
        ElapsedTime = 0;
        StateChangeTime = 0.5f;
        monster.PlayAnimation("Ani_State", (int)MONSTER_ANI_STATE.ATTACK);
    }

    public override void OnStateExit()
    {
        ElapsedTime = 0;
        //Debug.Log("OnAttackExit");
    }

    public override void OnStateUpdate()
    {
        if (monster.IsPlayerInAttackRange() == false) //플레이거가 공격사거리 밖으로 벗어날 경우
        {
            ElapsedTime += Time.deltaTime;
            if(ElapsedTime >= StateChangeTime)
            {
                monster.ChangeState((int)MONSTER_STATE.CHASE);
            }
        }
    }
}
