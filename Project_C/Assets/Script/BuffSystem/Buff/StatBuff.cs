using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class StatBuff : Buff
{
    public float StatIncrease;
    STAT_TYPE StatType;
    public StatBuff(STAT_TYPE _stattype, float _statIncrease, float _duration, GameObject _target,string _bufficonName) : base(_duration, _target, _bufficonName)
    {
        StatIncrease = _statIncrease;
        StatType = _stattype;
    }

    public override void ApplyBuff()
    {
        Target.GetComponent<StatData>().EnhanceStat(StatType, StatIncrease);
    }

    public override void EndBuff()
    {
        Target.GetComponent<StatData>().EnhanceStat(StatType, -StatIncrease);
    }

    public override void UpdateBuff()
    {

    }
}
