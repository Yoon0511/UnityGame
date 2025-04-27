 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character
{
    protected StateMachine Fsm = new StateMachine();
    protected int CurrState;
    protected int PrevState;
    protected Dictionary<int, StateBase> DicState = new Dictionary<int, StateBase>();
    public abstract void Fsm_Init();

    public void ChangeState(int _state)
    {
        if (CurrState == _state)
            return;

        PrevState = CurrState;
        CurrState = _state;

        ChangeFsm();
    }

    public void ChangeState(int _state,int _anistate)
    {
        if (CurrState == _state)
            return;
        PrevState = CurrState;
        CurrState = _state;

        PlayAnimation("Ani_State", _anistate);

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
