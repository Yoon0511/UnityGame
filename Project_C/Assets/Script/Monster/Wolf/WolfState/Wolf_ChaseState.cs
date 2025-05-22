using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_ChaseState : StateBase //타겟을 쫒아가는 상태
{
    Wolf Wolf;
    float OrgSpeed;
    float RunSpeed;
    public Wolf_ChaseState(Wolf _wolf)
    {
        Wolf = _wolf;
    }
    public override void OnStateEnter()
    {
        OrgSpeed = Wolf.GetSpeed();
        RunSpeed = OrgSpeed * 1.5f;
        Wolf.SetSpeed(RunSpeed);
        Wolf.PlayAnimation("Ani_State", (int)MONSTER_ANI_STATE.RUN);
    }

    public override void OnStateExit()
    {
        Wolf.SetSpeed(OrgSpeed);
        //Debug.Log("OnIdleExit");
    }

    public override void OnStateUpdate()
    {
        Wolf.MoveToTarget();

        if (Wolf.IsPlayerInAttackRange()) //플레이어가 공격범위 안으로 들어옴
            Wolf.ChangeState((int)MONSTER_ANI_STATE.ATTACK);

        if (Wolf.IsPlayerInDetectionRange() == false &&
            Wolf.GetIsLeader() == true) //플레이어 감지범위 밖으로 나감
        {
            Shared.UiMgr.CreateSpeiclMark("Question_mark", 1.0f, Wolf.GetUiHead());
            Wolf.ChangeState((int)MONSTER_STATE.PATROL);
        }
    }
}
