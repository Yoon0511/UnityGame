using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character
{
    [SerializeField]
    Animator Animator;

    public void PlayAnimation(string _parametername,int _state)
    {
        Animator.SetInteger(_parametername, _state);
    }
    public void PlayAnimation(STATE _state) //사용하려면 형변환 T -> value
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
}
