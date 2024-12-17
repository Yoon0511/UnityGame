using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMove : StateBase
{
    public override void OnStateEnter()
    {
        Debug.Log("OnMoveEnter");
    }

    public override void OnStateExit()
    {
        Debug.Log("OnMoveExit");
    }

    public override void OnStateUpdate()
    {
        Debug.Log("OnMoveUpdate");
    }
}
