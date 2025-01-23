using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_IdleState : StateBase
{
    Dragon Dragon;
    public Dragon_IdleState(Dragon _dragon)
    {
        Dragon = _dragon;
    }
    public override void OnStateEnter()
    {
        Dragon.FsmUseDragonSkill((int)DRAGON_SKILL.RUSH);

        Dragon.PlayAnimation("Ani_State", (int)DRAGON_STATE.IDLE);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        
    }
}
