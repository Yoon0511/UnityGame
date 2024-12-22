using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    Animator animator;

    public void PlayAni_Trigger(string _trigger)
    {
        animator.SetTrigger(_trigger);
    }

    public void PlayAni_float(string _trigger,float _value)
    {
        animator.SetFloat(_trigger, _value);
    }

    public void AniATKEnd()
    {
        ChangeState(Prev_State);
    }
}
