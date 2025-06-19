using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_DieState : StateBase
{
    Dragon Dragon;
    public Dragon_DieState(Dragon _dragon)
    {
        Dragon = _dragon;
    }
    public override void OnStateEnter()
    {
        Dragon.SetIsDead(true);
        Dragon.PlayAnimation("Ani_State", (int)DRAGON_ANI_STATE.DIE);
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {

    }
}
