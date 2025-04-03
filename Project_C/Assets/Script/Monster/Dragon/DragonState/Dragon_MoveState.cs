using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_MoveState : StateBase
{
    Dragon Dragon;
    public Dragon_MoveState(Dragon _dragon)
    {
        Dragon = _dragon;
    }
    public override void OnStateEnter()
    {
        Dragon.PlayAnimation("Ani_State", (int)DRAGON_STATE.MOVE);

        SetTarget();
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        Dragon.MoveToTarget();

        if(Dragon.IsPlayerInAttackRange())
        {
            Dragon.ChangeState((int)DRAGON_STATE.ATTACK);
        }
    }

    void SetTarget()
    {
        Dragon.ChangeTarget(Shared.GameMgr.PLAYEROBJ);
    }
}
