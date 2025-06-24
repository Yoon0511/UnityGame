using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_ChaseState : StateBase //타겟을 쫒아가는 상태
{
    Wolf Wolf;
    float OrgSpeed;
    float RunSpeed;
    float SpeedIncrease;
    float ChaseElapsedTime;
    float ChaseTime;
    
    public Wolf_ChaseState(Wolf _wolf)
    {
        Wolf = _wolf;
    }
    public override void OnStateEnter()
    {
        ChaseTime = 5.0f;
        ChaseElapsedTime = 0;
        SpeedIncrease = 2.3f;

        OrgSpeed = Wolf.GetSpeed();
        RunSpeed = OrgSpeed * SpeedIncrease;
        Wolf.SetSpeed(RunSpeed);

        Wolf.PlayAnimation("Ani_State", (int)MONSTER_ANI_STATE.RUN);
        Shared.UiMgr.CreateSpeiclMark("Exclamation_mark", 1.0f, Wolf.GetUiHead());
    }

    public override void OnStateExit()
    {
        ChaseElapsedTime = 0;
        Wolf.SetSpeed(OrgSpeed);
        //Debug.Log("OnIdleExit");
    }

    public override void OnStateUpdate()
    {
        Wolf.MoveToTarget();

        ChaseElapsedTime += Time.deltaTime;
        //ChaseTime(추적시간) 시간안에 공격상태가 되지 않으면 정찰상태로 변경
        if (ChaseElapsedTime >= ChaseTime)
        {
            Shared.UiMgr.CreateSpeiclMark("Question_mark", 1.0f, Wolf.GetUiHead());
            Wolf.ChangeState((int)MONSTER_STATE.PATROL);
        }

        if (Wolf.IsPlayerInAttackRange()) //플레이어가 공격범위 안으로 들어옴
            Wolf.ChangeState((int)MONSTER_STATE.ATTACK);

        if (Wolf.IsPlayerInDetectionRange() == false &&
            Wolf.GetPrevState() == (int)MONSTER_STATE.ATTACK) //플레이어 감지범위 밖으로 나감
        {
            //자신의 상태를 정찰로 변경
            Shared.UiMgr.CreateSpeiclMark("Question_mark", 1.0f, Wolf.GetUiHead());
            Wolf.ChangeState((int)MONSTER_STATE.PATROL);
        }
    }
}
