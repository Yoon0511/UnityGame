using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_IdleState : StateBase
{
    Dragon Dragon;
    float ElapsedTime = 0.0f;
    float ChangeStateTime = 0.0f;
    public Dragon_IdleState(Dragon _dragon)
    {
        Dragon = _dragon;
    }
    public override void OnStateEnter()
    {
        Dragon.PlayAnimation("Ani_State", (int)DRAGON_STATE.IDLE);

        ChangeStateTime = 5.0f;
        //Dragon.UseDragonSkill((int)DRAGON_SKILL.BREATH);
    }

    public override void OnStateExit()
    {
        ElapsedTime = 0;
    }

    public override void OnStateUpdate()
    {
        ElapsedTime += Time.deltaTime;
        if (ElapsedTime >= ChangeStateTime)
        {
            Dragon.ChangeState((int)DRAGON_STATE.MOVE);
            ElapsedTime = 0f;
        }
    }
}
