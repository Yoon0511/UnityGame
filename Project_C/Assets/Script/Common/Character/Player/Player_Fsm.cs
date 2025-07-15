using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Cockpit;
using UnityEngine;
public partial class Player : Character
{
    bool IsAutoMode = false;
    List<Monster> ListMonster;
    public override void Fsm_Init()
    {
        Fsm = new StateMachine(new Player_IdleState(this));
        IsAutoMode = false;

        CurrState = (int)STATE.IDLE;
        PrevState = CurrState;
        
        //수동조작 상태
        DicState.Add((int)STATE.IDLE, new Player_IdleState(this));
        DicState.Add((int)STATE.WALK, new Player_WalkState(this));
        DicState.Add((int)STATE.RUN, new Player_RunState(this));
        DicState.Add((int)STATE.ATTACK, new Player_AttackState(this));
        DicState.Add((int)STATE.DIE, new Player_DieState(this));
        DicState.Add((int)STATE.RIDING, new Player_RidingState(this));
        DicState.Add((int)STATE.AUTOMOVE, new Player_AutoChaseState(this));

        //자동사냥 상태
        DicState.Add((int)AUTO_STATE.CHASE, new Player_AutoChaseState(this));
        DicState.Add((int)AUTO_STATE.ATTACK, new Player_AutoAttackState(this));

        Fsm.ChangeState(DicState[(int)STATE.IDLE]);
    }

    void StateChageJoystick(float _speed)
    {
        //자동이동 중 일때와 자동사냥 모드 시 상태변화 안함
        if(IsAutoMove || IsAutoMode)
        {
            return;
        }

        float baseSpeed = Statdata.GetData(STAT_TYPE.SPEED);
        int newState;

        if (_speed >= baseSpeed * 0.5f)
        {
            newState = IsRiding ? (int)RIDING_STATE.RUN : (int)STATE.RUN;
        }
        else if (_speed > baseSpeed * 0.2f)
        {
            newState = IsRiding ? (int)RIDING_STATE.WALK : (int)STATE.WALK;
        }
        else
        {
            newState = IsRiding ? (int)RIDING_STATE.IDLE : (int)STATE.IDLE;
        }

        if (IsRiding)
            Riding.ChangeState(newState);
        else
            ChangeState(newState);
    }

    /////////////////// 범위 테스트 ///////////////////
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        //float dist = Vector3.Distance(transform.position, Target.transform.position);
        //Debug.Log(dist);
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRange);
    }
    /////////////////// 범위 테스트 ///////////////////
    

    //가장 가까운 몬스터 탐색
    public void FindNearMonster()
    {
        int Nearindex = 0;

        float MinDist = float.MaxValue;
        for (int i = 0; i < ListMonster.Count; ++i)
        {
            if(ListMonster[i] == null || ListMonster[i].GetIsDead() == true)
            {
                continue;
            }

            float dist = DistXZ(transform.position, ListMonster[i].transform.position);
            if (dist < MinDist)
            {
                MinDist = dist;
                Nearindex = i;
            }
        }
        ChangeTarget(ListMonster[Nearindex].gameObject);
    }

    //근처 몬스터 탐색
    public bool IsMonsterInDetectionRange()
    {
        for (int i = 0; i < ListMonster.Count; ++i)
        {
            float dist = DistXZ(ListMonster[i].transform.position, transform.position);
            if (dist <= DetectionRange)
            {
                ChangeTarget(ListMonster[i].gameObject);
                return true;
            }
        }
        return false;
    }

    //공격범위 내 공격
    public bool IsMonsterInAttackRange()
    {
        for (int i = 0; i < ListMonster.Count; ++i)
        {
            float dist = DistXZ(ListMonster[i].transform.position, transform.position);
            if (dist <= AttackRange)
            {
                ChangeTarget(ListMonster[i].gameObject);
                return true;
            }
        }
        return false;
    }

    public void OnSwitchAutoMode()
    {
        IsAutoMode = !IsAutoMode;

        if(IsAutoMode)
        {
            OnStartAutoMode();
        }
        else
        {
            OnEndAutoMode();
        }
    }
    public bool GetIsAutoMode()
    {
        return IsAutoMode;
    }

    void OnStartAutoMode()
    {
        ChangeState((int)AUTO_STATE.CHASE);
    }

    void OnEndAutoMode()
    {
        ChangeState((int)STATE.IDLE);
    }
}
