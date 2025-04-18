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
        Dragon.PlayAnimation("Ani_State", (int)DRAGON_ANI_STATE.FORWARD_MOVE);
        //Dragon.ChangeAnimationWaitForSecond("Ani_State",(int)DRAGON_ANI_STATE.FORWARD_MOVE,0.3f);

        SetTarget();
    }

    public override void OnStateExit()
    {
        Dragon.PlayAnimation("Ani_State", (int)DRAGON_ANI_STATE.IDLE);
        Dragon.SetIsAniRunning(false);
    }

    public override void OnStateUpdate()
    {
        Dragon.MoveToTarget();

        if(Dragon.IsPlayerInAttackRange())
        {
            Dragon.ChangeState((int)DRAGON_STATE.ATTACK);
        }

        if(Dragon.GetCurrAnimation() != (int)DRAGON_ANI_STATE.FORWARD_MOVE)
        {
            Dragon.PlayAnimation("Ani_State", (int)DRAGON_ANI_STATE.FORWARD_MOVE);
        }
    }

    void SetTarget()
    {
        Dragon.ChangeTarget(Shared.GameMgr.PLAYEROBJ);
    }
}
