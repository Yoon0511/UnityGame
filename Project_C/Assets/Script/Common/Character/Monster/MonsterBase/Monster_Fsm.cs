using System.Collections;
using System.Collections.Generic;
using Photon.Pun.Demo.Cockpit;
using UnityEngine;

public partial class Monster
{
    public override void Fsm_Init()
    {
        Fsm = new StateMachine(new Monster_IdleState(this));
        CurrState = (int)MONSTER_STATE.IDLE;
        PrevState = CurrState;

        DicState.Add((int)MONSTER_STATE.IDLE, new Monster_IdleState(this));
        DicState.Add((int)MONSTER_STATE.MOVE, new Monster_MoveState(this));
        DicState.Add((int)MONSTER_STATE.PATROL, new Monster_PatrolState(this));
        DicState.Add((int)MONSTER_STATE.CHASE, new Monster_ChaseState(this));
        DicState.Add((int)MONSTER_STATE.ATTACK, new Monster_AttackState(this));
        DicState.Add((int)MONSTER_STATE.DIE, new Monster_DieState(this));

        Fsm.ChangeState(DicState[(int)MONSTER_STATE.IDLE]);

        PathNodeInit();
    }

    public bool IsPlayerInDetectionRange()
    {
        for(int i = 0; i < ListPlayer.Count; ++i)
        {
            float dist = DistXZ(ListPlayer[i].transform.position, transform.position);
            if (dist <= detectionRange)
            {
                ChangeTarget(ListPlayer[i].gameObject);
                return true;
            }
        }

        //float dist = DistXZ(ListPlayer[i].transform.position, transform.position);
        //if (dist <= detectionRange)
        //{
        //    return true;
        //}

        return false;
    }

    /////////////////// 범위 테스트 ///////////////////
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    //float dist = Vector3.Distance(transform.position, Target.transform.position);
    //    //Debug.Log(dist);
    //    Gizmos.DrawSphere(transform.position, detectionRange);
    //
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, attackRange);
    //}
    /////////////////// 범위 테스트 ///////////////////
    public bool IsPlayerInAttackRange()
    {
        //float dist = DistXZ(Target.transform.position, transform.position);
        //if (dist <= attackRange)
        //{
        //    return true;
        //}

        for (int i = 0; i < ListPlayer.Count; ++i)
        {
            float dist = DistXZ(ListPlayer[i].transform.position, transform.position);
            if (dist <= attackRange)
            {
                return true;
            }
        }
        return false;
    }

    public void PatrolModeInit()
    {
        SetPathNodeIndex(0);
    }
    public void PatrolMode()
    {
        PathNodeUpdate();
    }

    public void SetPatrolIndex(int _index)
    {
        SetPathNodeIndex(_index);
    }

    public void StartChageToPatrol(float _time)
    {
        StartCoroutine(IChageToPatrol(_time));
    }
    private IEnumerator IChageToPatrol(float _time)
    {
        yield return new WaitForSeconds(_time);
        ChangeState((int)MONSTER_STATE.PATROL);
    }


}
