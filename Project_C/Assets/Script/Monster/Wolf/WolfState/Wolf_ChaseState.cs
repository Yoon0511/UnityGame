using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf_ChaseState : StateBase //Ÿ���� �i�ư��� ����
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
        //ChaseTime(�����ð�) �ð��ȿ� ���ݻ��°� ���� ������ �������·� ����
        if (ChaseElapsedTime >= ChaseTime)
        {
            Shared.UiMgr.CreateSpeiclMark("Question_mark", 1.0f, Wolf.GetUiHead());
            Wolf.ChangeState((int)MONSTER_STATE.PATROL);
        }

        if (Wolf.IsPlayerInAttackRange()) //�÷��̾ ���ݹ��� ������ ����
            Wolf.ChangeState((int)MONSTER_STATE.ATTACK);

        if (Wolf.IsPlayerInDetectionRange() == false &&
            Wolf.GetPrevState() == (int)MONSTER_STATE.ATTACK) //�÷��̾� �������� ������ ����
        {
            //�ڽ��� ���¸� ������ ����
            Shared.UiMgr.CreateSpeiclMark("Question_mark", 1.0f, Wolf.GetUiHead());
            Wolf.ChangeState((int)MONSTER_STATE.PATROL);
        }
    }
}
