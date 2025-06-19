using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character
{
    public void IsCritical(ref DamageData _damagedata)
    {
        float CiritcalCance = GetInStatData(STAT_TYPE.CRITICAL_CANCE);
        float CiritcalMultiplier = GetInStatData(STAT_TYPE.CRITICAL_MULTIPLIER);

        bool isCritical = Random.value < CiritcalCance;

        if(isCritical)
        {
            _damagedata.Damage *= (int)CiritcalMultiplier;
            _damagedata.DamageFont_Type = DAMAGEFONT_TYPE.CRITICAL;
        }
    }
}
