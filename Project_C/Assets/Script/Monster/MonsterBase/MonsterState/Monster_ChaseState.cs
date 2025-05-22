using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_ChaseState : StateBase //타겟을 쫒아가는 상태
{
    Monster monster;
    float OrgSpeed;
    float RunSpeed;
    public Monster_ChaseState(Monster _monster)
    {
        monster = _monster;
    }
    public override void OnStateEnter()
    {
        OrgSpeed = monster.GetSpeed();
        RunSpeed = OrgSpeed * 1.5f;
        monster.SetSpeed(RunSpeed);
        monster.PlayAnimation("Ani_State", (int)MONSTER_ANI_STATE.RUN);
    }

    public override void OnStateExit()
    {
        monster.SetSpeed(OrgSpeed);
        //Debug.Log("OnIdleExit");
    }

    public override void OnStateUpdate()
    {
        monster.MoveToTarget();

        if (monster.IsPlayerInAttackRange()) //플레이어가 공격범위 안으로 들어옴
            monster.ChangeState((int)MONSTER_STATE.ATTACK);

        if (monster.IsPlayerInDetectionRange() == false) //플레이어 감지범위 밖으로 나감
        {
            Shared.UiMgr.CreateSpeiclMark("Question_mark", 1.0f, monster.GetUiHead());
            monster.ChangeState((int)MONSTER_STATE.PATROL);
        }
        //Debug.Log("OnIdleUpdate");
    }
}
