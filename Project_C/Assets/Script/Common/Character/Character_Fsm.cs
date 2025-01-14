 using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract partial class Character<T>
{
    protected StateMachine Fsm = new StateMachine();
    protected T CurrState;
    protected T PrevState;
    protected Dictionary<T, StateBase> DicState = new Dictionary<T, StateBase>();

    public abstract void Fsm_Init();

    public void ChangeState(T _state)
    {
        if (CurrState.Equals(_state))
            return;

        PrevState = CurrState;
        CurrState = _state;

        ChangeFsm();
    }

    public void ChangeState(T _state,int _anistate)
    {
        if (CurrState.Equals(_state))
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
}
