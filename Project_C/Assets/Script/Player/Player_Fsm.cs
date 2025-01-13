using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public partial class Player : Character<STATE>
{
    public override void Fsm_Init()
    {
        Fsm = new StateMachine(new Player_IdleState(this));

        CurrState = STATE.IDLE;
        PrevState = CurrState;
        
        DicState.Add(STATE.IDLE, new Player_IdleState(this));
        DicState.Add(STATE.WALK, new Player_WalkState(this));
        DicState.Add(STATE.RUN, new Player_RunState(this));
        DicState.Add(STATE.ATTACK, new Player_AttackState(this));
        DicState.Add(STATE.DIE, new Player_DieState(this));
        
        Fsm.ChangeState(DicState[STATE.IDLE]);
    }

    void StateChageJoystick(float _speed)
    {
        float speed = Statdata.GetData(STAT_TYPE.SPEED);
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
