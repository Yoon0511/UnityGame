using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Riding
{
    protected StateMachine Fsm = new StateMachine();
    protected int CurrState;
    protected int PrevState;
    protected Dictionary<int, StateBase> DicState = new Dictionary<int, StateBase>();

    void FsmInit()
    {
        CurrState = (int)RIDING_STATE.IDLE;
        PrevState = CurrState;

        DicState.Add((int)RIDING_STATE.IDLE, new Riding_Idle(this));
        DicState.Add((int)RIDING_STATE.WALK, new Riding_Walk(this));
        DicState.Add((int)RIDING_STATE.RUN, new Riding_Run(this));

        Fsm.ChangeState(DicState[(int)RIDING_STATE.IDLE]);
    }


    public void ChangeState(int _state)
    {
        if (CurrState == _state)
            return;

        PrevState = CurrState;
        CurrState = _state;

        ChangeFsm();
    }

    public void ChangeState(int _state, int _anistate)
    {
        if (CurrState == _state)
            return;
        PrevState = CurrState;
        CurrState = _state;

        ChangeFsm();
    }

    public void ChangeFsm()
    {
        Fsm.ChangeState(DicState[CurrState]);
    }

    public void StateUpdate()
    {
        Fsm.UpdateState();
    }

    public int GetCurrState() { return CurrState; }
    public int GetPrevState() { return PrevState; }
}
