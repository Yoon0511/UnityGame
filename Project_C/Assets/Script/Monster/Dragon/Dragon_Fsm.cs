using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Dragon
{
    public override void Fsm_Init()
    {
        Fsm = new StateMachine(new Dragon_IdleState(this));
        CurrState = (int)DRAGON_STATE.IDLE;
        PrevState = CurrState;

        DicState.Add((int)DRAGON_STATE.IDLE, new Dragon_IdleState(this));
        DicState.Add((int)DRAGON_STATE.MOVE, new Dragon_MoveState(this));
        DicState.Add((int)DRAGON_STATE.ATTACK, new Dragon_AttackState(this));
        DicState.Add((int)DRAGON_STATE.DIE, new Dragon_DieState(this));

        Fsm.ChangeState(DicState[(int)DRAGON_STATE.IDLE]);
    }

    public void UseBreath()
    {
        StartCoroutine(IUseBreath(2f));
    }

    IEnumerator IUseBreath(float _time)
    {
        yield return new WaitForSeconds(_time);
        StartCoroutine(Dragon_Braeth());
    }
}