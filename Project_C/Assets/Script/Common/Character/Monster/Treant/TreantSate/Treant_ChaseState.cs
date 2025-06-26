using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treant_ChaseState : StateBase
{
    Treant Treant;
    public Treant_ChaseState(Treant _treant)
    {
        Treant = _treant;
    }
    public override void OnStateEnter()
    {
        Treant.PlayAnimation("Ani_State", (int)TREANT_ANI_STATE.RUN);
        Treant.ChangeTarget(Shared.GameMgr.PLAYEROBJ);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        Treant.MoveToTarget();

        if(Treant.IsPlayerInAttackRange())
        {
            Treant.ChangeState((int)TREANT_STATE.ATTACK);
        }
    }
}
