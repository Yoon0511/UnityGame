using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff
{
    public float Duration;
    public GameObject Target;
    public string BuffIconName;

    public Buff(float _duration, GameObject _target, string buffIconName)
    {
        Duration = _duration;
        Target = _target;
        BuffIconName = buffIconName;
    }

    public abstract void ApplyBuff();
    public abstract void EndBuff();
    public abstract void UpdateBuff();
}
