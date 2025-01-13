using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff
{
    public float Duration;
    public GameObject Target;

    public Buff(float _duration, GameObject _target)
    {
        Duration = _duration;
        Target = _target;
    }

    public abstract void ApplyBuff();
    public abstract void EndBuff();
}
