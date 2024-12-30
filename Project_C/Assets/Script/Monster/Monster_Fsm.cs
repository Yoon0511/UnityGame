using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    StateMachine fsm;

    [SerializeField]
    MONSTER_STATE curr_state;
    MONSTER_STATE prev_state;
    Dictionary<MONSTER_STATE, StateBase> dicState = new Dictionary<MONSTER_STATE, StateBase>();

    public List<GameObject> patrolPoint = new List<GameObject>();
    int patrolIndex = 0;
    void Fsm_Init()
    {
        fsm = new StateMachine(new Monster_IdleState(this));
        curr_state = MONSTER_STATE.IDLE;
        prev_state = curr_state;

        dicState.Add(MONSTER_STATE.IDLE, new Monster_IdleState(this));
        dicState.Add(MONSTER_STATE.MOVE, new Monster_MoveState(this));
        dicState.Add(MONSTER_STATE.PATROL, new Monster_PatrolState(this));
        dicState.Add(MONSTER_STATE.CHASE, new Monster_ChaseState(this));
        dicState.Add(MONSTER_STATE.ATTACK, new Monster_AttackState(this));
        dicState.Add(MONSTER_STATE.DIE, new Monster_DieState(this));

        fsm.ChangeState(dicState[MONSTER_STATE.IDLE]);
    }

    public void ChangeState(MONSTER_STATE _state)
    {
        if (curr_state == _state)
            return;

        prev_state = curr_state;
        curr_state = _state;

        fsm.ChangeState(dicState[curr_state]);
    }

    public void StateUpdate()
    {
        fsm.UpdateState();
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
