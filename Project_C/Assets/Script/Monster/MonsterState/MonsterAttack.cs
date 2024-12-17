using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : StateBase
{
    public override void OnStateEnter()
    {
        Debug.Log("OnAttackEnter");
    }

    public override void OnStateExit()
    {
        Debug.Log("OnAttackExit");
    }

    public override void OnStateUpdate()
    {
        Debug.Log("OnAttackUpdate");
    }
}
