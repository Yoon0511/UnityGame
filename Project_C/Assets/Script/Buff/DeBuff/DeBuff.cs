using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBuff : Buff
{
    protected DEBUFF_TYPE DebuffType;
    public DeBuff(float _duration, GameObject _target, string _buffIconName) : base(_duration, _target, _buffIconName)
    {

    }
    public override void ApplyBuff()
    {
        
    }

    public override void EndBuff()
    {
        
    }

    public DEBUFF_TYPE GetDebuffType() { return DebuffType; }
}