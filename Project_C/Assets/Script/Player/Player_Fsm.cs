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
        //float speed = Statdata.GetData(STAT_TYPE.SPEED);
        //
        //if (_speed >= speed * 0.5f)
        //{
        //    if(IsRiding)
        //    {
        //        Debug.Log($"run = {_speed}");
        //        Riding.ChangeState((int)RIDING_STATE.RUN);
        //    }
        //    else
        //    {
        //        ChangeState((int)STATE.RUN);
        //    }
        //}
        //else if (_speed > speed * 0.2f)
        //{
        //    if (IsRiding)
        //    {
        //        Riding.ChangeState((int)RIDING_STATE.WALK);
        //    }
        //    else
        //    {
        //        ChangeState((int)STATE.WALK);
        //    }
        //}
        //else
        //{
        //    if (IsRiding)
        //    {
        //        Debug.Log($"idle = {_speed}");
        //        Riding.ChangeState((int)RIDING_STATE.IDLE);
        //    }
        //    else
        //    {
        //        ChangeState((int)STATE.IDLE);
        //    }
        //}
        float baseSpeed = Statdata.GetData(STAT_TYPE.SPEED);
        int newState;
        //Debug.Log($"[Speed] {_speed}, [Thresholds] run: {_speed * 0.5f}, walk: {_speed * 0.2f}");
        if (_speed >= baseSpeed * 0.5f)
        {
            newState = IsRiding ? (int)RIDING_STATE.RUN : (int)STATE.RUN;
            //if (IsRiding) Debug.Log($"run = {_speed}");
        }
        else if (_speed > baseSpeed * 0.2f)
        {
            newState = IsRiding ? (int)RIDING_STATE.WALK : (int)STATE.WALK;
        }
        else
        {
            newState = IsRiding ? (int)RIDING_STATE.IDLE : (int)STATE.IDLE;
            //if (IsRiding) Debug.Log($"idle = {_speed}");
        }

        if (IsRiding)
            Riding.ChangeState(newState);
        else
            ChangeState(newState);
    }
}
