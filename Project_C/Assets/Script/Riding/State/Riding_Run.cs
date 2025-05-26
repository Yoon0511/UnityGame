using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riding_Run : StateBase
{
    Riding Riding;
    public Riding_Run(Riding _riding)
    {
        Riding = _riding;
    }
    public override void OnStateEnter()
    {
        Riding.GetOwner().Statdata.SetStat(STAT_TYPE.SPEED, Riding.GetRunSpeed());
        Riding.PlayAnimation((int)RIDING_STATE.RUN);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}
