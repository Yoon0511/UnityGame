using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldBuff : StatBuff
{
    public ShieldBuff(STAT_TYPE _stattype, float _statIncrease, float _duration, GameObject _target, string _bufficonName) : base(_stattype, _statIncrease, _duration, _target, _bufficonName)
    {
    }

    public override void ApplyBuff()
    {
        Target.GetComponent<StatData>().EnhanceStat(StatType, StatIncrease);
    }

    public override void EndBuff()
    {
        Target.GetComponent<StatData>().SetStat(StatType,0);
    }

    public override void UpdateBuff()
    {

    }
}
