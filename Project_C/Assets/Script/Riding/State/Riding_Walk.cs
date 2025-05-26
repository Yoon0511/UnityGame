using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riding_Walk : StateBase
{
    Riding Riding;

    public Riding_Walk(Riding _riding)
    {
        Riding = _riding;
    }
    public override void OnStateEnter()
    {
        Riding.GetOwner().Statdata.SetStat(STAT_TYPE.SPEED, Riding.GetWalkSpeed());
        Riding.PlayAnimation((int)RIDING_STATE.WALK);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}
