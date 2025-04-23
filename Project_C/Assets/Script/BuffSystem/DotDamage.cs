using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotDamage : StatBuff
{
    float DotInterval;
    float ElapsedTime;
    Character DotTarget;

    public DotDamage(float _DotInterval, STAT_TYPE _stattype, float _statIncrease, float _duration, GameObject _target, string _bufficonName) : base(_stattype, _statIncrease, _duration, _target, _bufficonName)
    {
        DotInterval = _DotInterval;
        DotTarget = _target.GetComponent<Character>();
    }

    public override void ApplyBuff()
    {
       
    }

    public override void EndBuff()
    {
        
    }

    public override void UpdateBuff()
    {
        ElapsedTime += Time.deltaTime;

        if(ElapsedTime >= DotInterval)
        {
            ElapsedTime = 0.0f;
            DotTarget.Hit(StatIncrease);
        }
    }
}
