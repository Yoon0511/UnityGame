using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character
{
    public void IsCritical(ref float _damage)
    {
        float CiritcalCance = GetInStatData(STAT_TYPE.CRITICAL_CANCE);
        float CiritcalMultiplier = GetInStatData(STAT_TYPE.CRITICAL_MULTIPLIER);

        bool isCritical = Random.value < CiritcalCance;

        if(isCritical)
        {
            _damage *= CiritcalMultiplier;
        }
    }
}
