using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character
{
    [SerializeField]
    Animator Animator;

    bool IsAniRunning = false;
    public void PlayAnimation(string _parametername,int _state)
    {
        Animator.SetInteger(_parametername, _state);
    }
    public void PlayAnimation(STATE _state)
    {
        Animator.SetInteger("Ani_State", (int)_state);
    }
    public void PlayAni_Trigger(string _trigger)
    {
        Animator.SetTrigger(_trigger);
    }

    public void PlayAni_float(string _trigger, float _value)
    {
        Animator.SetFloat(_trigger, _value);
    }

    public void ChangeAnimationWaitForSecond(string _parametername, int _state,float _time)
    {
        StartCoroutine(IChangeAnimationWaitForSecond(_parametername,_state,_time));
    }

    IEnumerator IChangeAnimationWaitForSecond(string _parametername, int _state, float _time)
    {
        yield return new WaitForSeconds(_time);
        Animator.SetInteger(_parametername, _state);
    }

    public void OnAniStart()
    {
        IsAniRunning = true;
    }
    public void OnAniEnd()
    {
        IsAniRunning = false;
    }

    public bool GetIsAniRunning() { return IsAniRunning; }
}
