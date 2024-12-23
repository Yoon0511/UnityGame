using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class Player
{
    StateMachine fsm;

    STATE curr_state;
    STATE prev_state;
    Dictionary<STATE, StateBase> dicState = new Dictionary<STATE, StateBase>();

    void Fsm_Init()
    {
        fsm = new StateMachine(new Player_IdleState(this));
        curr_state = STATE.IDLE;
        prev_state = curr_state;

        dicState.Add(STATE.IDLE, new Player_IdleState(this));
        dicState.Add(STATE.WALK, new Player_WalkState(this));
        dicState.Add(STATE.RUN, new Player_RunState(this));
        dicState.Add(STATE.ATTACK, new Player_AttackState(this));
        dicState.Add(STATE.DIE, new Player_DieState(this));

        fsm.ChangeState(dicState[STATE.IDLE]);
    }

    public void ChangeState(STATE _state)
    {
        if (curr_state == _state)
            return;

        prev_state = curr_state;
        curr_state = _state;

        ChangeFsm();
    }

    public void ChangeFsm()
    {
        fsm.ChangeState(dicState[curr_state]);
    }

    public void StateUpdate()
    {
        fsm.UpdateState();
    }

    void StateChageJoystick(float _speed)
    {
        if (_speed >= speed * 0.6f)
        {
            ChangeState(STATE.RUN);
        }
        else if (_speed > speed * 0.4f)
        {
            ChangeState(STATE.WALK);
        }
        else
        {
            ChangeState(STATE.IDLE);
        }
    }
}
