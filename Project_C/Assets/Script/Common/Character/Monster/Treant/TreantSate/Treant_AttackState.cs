using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treant_AttackState : StateBase
{
    Treant Treant;
    public Treant_AttackState(Treant _treant)
    {
        Treant = _treant;
    }
    public override void OnStateEnter()
    {
        Treant.PlayAnimation("Ani_State", (int)TREANT_ANI_STATE.ATTACK);
    }

    public override void OnStateExit()
    {
       
    }

    public override void OnStateUpdate()
    {
        if (Treant.IsPlayerInAttackRange() == false)
        {
            Treant.ChangeState((int)TREANT_STATE.CHASE);
        }
    }
}
