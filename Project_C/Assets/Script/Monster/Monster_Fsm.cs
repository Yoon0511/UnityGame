using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Monster
{
    public List<GameObject> patrolPoint = new List<GameObject>();
    int patrolIndex = 0;
    public override void Fsm_Init()
    {
        Debug.Log("monster_fsm_init");
        Fsm = new StateMachine(new Monster_IdleState(this));
        CurrState = MONSTER_STATE.IDLE;
        PrevState = CurrState;

        DicState.Add(MONSTER_STATE.IDLE, new Monster_IdleState(this));
        DicState.Add(MONSTER_STATE.MOVE, new Monster_MoveState(this));
        DicState.Add(MONSTER_STATE.PATROL, new Monster_PatrolState(this));
        DicState.Add(MONSTER_STATE.CHASE, new Monster_ChaseState(this));
        DicState.Add(MONSTER_STATE.ATTACK, new Monster_AttackState(this));
        DicState.Add(MONSTER_STATE.DIE, new Monster_DieState(this));

        Fsm.ChangeState(DicState[MONSTER_STATE.IDLE]);
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

    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.blue;
    //    float dist = Vector3.Distance(transform.position, Target.transform.position);
    //    Debug.Log(dist);
    //    Gizmos.DrawSphere(transform.position, dist);

    //    Gizmos.color = Color.red;
    //    Gizmos.DrawSphere(transform.position, 1f);
    //}

    public bool IsPlayerInAttackRange()
    {
        float dist = Vector3.Distance(player.transform.position, transform.position);
        if (dist <= attackRange)
        {
            return true;
        }
        return false;
    }

    public void PatrolModeInit()
    {
        patrolIndex = 0;
        ChageTarget(patrolPoint[patrolIndex]);
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
            ChageTarget(patrolPoint[patrolIndex]);
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
        ChangeState(MONSTER_STATE.PATROL);
    }
}
