using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    public List<GameObject> patrolPoint = new List<GameObject>();
    int patrolIndex = 0;
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
    }

    public bool IsPlayerInDetectionRange()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist <= detectionRange)
        {
            return true;
        }
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
        float dist = Vector3.Distance(Target.transform.position, transform.position);
        if (dist <= attackRange)
        {
            return true;
        }
        return false;
    }

    public void PatrolModeInit()
    {
        patrolIndex = 0;
        ChangeTarget(patrolPoint[patrolIndex]);
    }
    public void PatrolMode()
    {
        MoveToTarget();

        float dist = Vector3.Distance(transform.position, patrolPoint[patrolIndex].transform.position);
        if (dist <= 0.5f)
        {
            patrolIndex++;
            if (patrolIndex >= patrolPoint.Count)
            {
                patrolIndex = 0;
            }
            ChangeTarget(patrolPoint[patrolIndex]);
        }
    }
    public void SetPatrolIndex(int _index)
    {
        patrolIndex = _index;
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
