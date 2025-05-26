using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riding_Idle : StateBase
{
    Riding Riding;

    public Riding_Idle(Riding _riding)
    {
        Riding = _riding;
    }

    public override void OnStateEnter()
    {
        Riding.PlayAnimation((int)RIDING_STATE.IDLE);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}
