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
        DicState.Add((int)STATE.RIDING, new Player_RidingState(this));
        
        Fsm.ChangeState(DicState[(int)STATE.IDLE]);
    }

    void StateChageJoystick(float _speed)
    {
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
}
