using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class Player : Character
{
    public override void Fsm_Init()
    {
        Fsm = new StateMachine(new Player_IdleState(this));

        CurrState = (int)STATE.IDLE;
        PrevState = CurrState;
        
        DicState.Add((int)STATE.IDLE, new Player_IdleState(this));
        DicState.Add((int)STATE.WALK, new Player_WalkState(this));
        DicState.Add((int)STATE.RUN, new Player_RunState(this));
        DicState.Add((int)STATE.ATTACK, new Player_AttackState(this));
        DicState.Add((int)STATE.DIE, new Player_DieState(this));
        
        Fsm.ChangeState(DicState[(int)STATE.IDLE]);
    }

    void StateChageJoystick(float _speed)
    {
        float speed = Statdata.GetData(STAT_TYPE.SPEED);
        if (_speed >= speed * 0.6f)
        {
            ChangeState((int)STATE.RUN);
        }
        else if (_speed > speed * 0.4f)
        {
            ChangeState((int)STATE.WALK);
        }
        else
        {
            ChangeState((int)STATE.IDLE);
        }
    }
}
