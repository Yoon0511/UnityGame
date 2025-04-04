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

    public void FsmUseDragonSkill(int _skillindex)
    {
        //StartCoroutine(IUseDragonSkill(2f, _skillindex));
        StartCoroutine(IUseDragonSkillWaitForSecond(2f, _skillindex));
    }

    IEnumerator IUseDragonSkillWaitForSecond(float _time, int _skillindex)
    {
        yield return new WaitForSeconds(_time);
        StartCoroutine(IUseDragonSkill(_skillindex));
    }

    public void UseDragonSkill(int _skillindex)
    {
        StartCoroutine(IUseDragonSkill(_skillindex));
    }
}