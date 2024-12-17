using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdle : StateBase
{
    public override void OnStateEnter()
    {
        Debug.Log("OnIdleEnter");
    }

    public override void OnStateExit()
    {
        Debug.Log("OnIdleExit");
    }

    public override void OnStateUpdate()
    {
        Debug.Log("OnIdleUpdate");
    }
}
