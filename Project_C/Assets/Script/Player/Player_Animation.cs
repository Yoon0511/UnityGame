using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField]
    Animator animator;

    public void PlayAnimation(int _state)
    {
        animator.SetInteger("Ani_State", _state);
    }
    public void PlayAnimation(STATE _state)
    {
        animator.SetInteger("Ani_State", (int)_state);
    }
    public void PlayAni_Trigger(string _trigger)
    {
        animator.SetTrigger(_trigger);
    }

    public void PlayAni_float(string _trigger,float _value)
    {
        animator.SetFloat(_trigger, _value);
    }
}
