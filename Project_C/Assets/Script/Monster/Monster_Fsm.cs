using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Monster
{
    StateMachine fsm;

    MONSTER_STATE curr_state;
    MONSTER_STATE prev_state;
    Dictionary<MONSTER_STATE, StateBase> dicState = new Dictionary<MONSTER_STATE, StateBase>();

    void Fsm_Init()
    {
        fsm = new StateMachine(new IdleState(this));
        curr_state = MONSTER_STATE.IDLE;
        prev_state = curr_state;

        dicState.Add(MONSTER_STATE.IDLE, new IdleState(this));
        dicState.Add(MONSTER_STATE.MOVE, new MoveState(this));
        dicState.Add(MONSTER_STATE.ATTACK, new AttackState(this));
        dicState.Add(MONSTER_STATE.DIE, new DieState(this));

        fsm.ChangeState(dicState[MONSTER_STATE.IDLE]);
    }

    public void ChageState(MONSTER_STATE _state)
    {
        if (curr_state == _state)
            return;

        prev_state = curr_state;
        curr_state = _state;

        ChageFsm();
    }

    public void ChageFsm()
    {
        fsm.ChangeState(dicState[curr_state]);
    }

    public void ChageFsm(MONSTER_STATE _state)
    {
        if (_state == MONSTER_STATE.NONE || _state == MONSTER_STATE.ENUM_END)
            return;

        fsm.ChangeState(dicState[_state]);
    }

    public void StateUpdate()
    {
        fsm.UpdateState();
    }

    public bool IsPlayerInDetectionRange()
    {
        float dist = Vector3.Distance(Target.transform.position, transform.position);
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
        float dist = Vector3.Distance(Target.transform.position, transform.position);
        if (dist <= attackRange)
        {
            return true;
        }
        return false;
    }
}
