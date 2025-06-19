using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_DieState : StateBase
{
    float RespwanTime;
    float RemoveTime;
    float RespwanElapsedTime;
    Monster monster;
    Vector3 SpwanPos;
    Vector3 DiePos;
    public Monster_DieState(Monster _monster)
    {
        monster = _monster;
    }
    public override void OnStateEnter()
    {
        RemoveTime = 3.0f; //3�ʵ� �����
        RespwanTime = 10.0f; //10�ʵ� ��Ȱ
        monster.PlayAnimation("Ani_State", (int)MONSTER_ANI_STATE.DIE);
        //Debug.Log("OnMoveEnter");
        SpwanPos = monster.transform.position; //���� ��ġ ����
        DiePos = new Vector3(800f, 0f, 800f);
        monster.SetIsDead(true);
    }

    public override void OnStateExit()
    {
        //Debug.Log("OnMoveExit");
        RespwanElapsedTime = 0.0f;
        monster.transform.position = SpwanPos;
        monster.EnhanceStat(STAT_TYPE.HP, monster.GetInStatData(STAT_TYPE.MAXHP));
        monster.SetIsDead(false);
    }

    public override void OnStateUpdate()
    {
        RespwanElapsedTime += Time.deltaTime;
        if(RespwanElapsedTime >= RemoveTime)
        {
            monster.transform.position = DiePos;
        }

        if(RespwanElapsedTime >= RespwanTime)
        {
            //������
            monster.ChangeState((int)MONSTER_STATE.IDLE);
        }
        //Debug.Log("OnMoveUpdate");
    }
}
