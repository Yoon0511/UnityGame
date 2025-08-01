using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character : Object
{
    [SerializeField]
    Animator Animator;

    protected bool IsAniRunning = false;
    int CurrAnimation;
    public void PlayAnimation(string _parametername,int _state)
    {
        CurrAnimation = _state;
        Animator.SetInteger(_parametername, _state);
    }
    public void PlayAnimation(STATE _state)
    {
        CurrAnimation = (int)_state;
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

    public void SetAnimatorBool(string _paraname,bool _value)
    {
        Animator.SetBool(_paraname, _value); 
    }

    public void ChangeAnimationWaitForSecond(string _parametername, int _state,float _time)
    {
        StartCoroutine(IChangeAnimationWaitForSecond(_parametername,_state,_time));
    }
    IEnumerator IChangeAnimationWaitForSecond(string _parametername, int _state, float _time)
    {
        yield return new WaitForSeconds(_time);
        PlayAnimation(_parametername, _state);
    }

    public virtual void OnAniStart()
    {
        IsAniRunning = true;
    }
    public virtual void OnAniEnd()
    {
        IsAniRunning = false;
        PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.IDLE);
    }

    public void AnimationStop() { Animator.StopPlayback(); }
    public bool GetIsAniRunning() { return IsAniRunning; }
    public void SetIsAniRunning(bool _value) { IsAniRunning = _value; }
    public int GetCurrAnimation() { return CurrAnimation; }
    public Animator GetAnimator() { return Animator; }
}
