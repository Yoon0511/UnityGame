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
        // Generic T 끼리 연산 해결해야함
       //if (CurrState == _state)
       //    return;

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
}
