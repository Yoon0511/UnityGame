using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_AttackState : StateBase
{
    Dragon Dragon;
    float ElapsedTime;
    float SkillDelayTime;
    float BasicAttackTime;
    float BasicAttackDelayTime;
    public Dragon_AttackState(Dragon _dragon)
    {
        Dragon = _dragon;
    }
    public override void OnStateEnter()
    {
        Debug.Log("Attack Enter");
        SkillDelayTime = 5.0f;
        BasicAttackDelayTime = 0.3f;

        Dragon.PlayAnimation("Ani_State", (int)DRAGON_ANI_STATE.IDLE);

        Dragon.ChangeAnimationWaitForSecond("Ani_State", (int)DRAGON_ANI_STATE.BITE_ATTACK,0.5f);
    }

    public override void OnStateExit()
    {
        Debug.Log("Attack Exit");
        ElapsedTime = 0;
        BasicAttackTime = 0;
    }

    public override void OnStateUpdate()
    {
        UseRandomSkill();

        //if(Dragon.IsPlayerInAttackRange() == false)
        //{
        //    Dragon.ChangeState((int)DRAGON_STATE.MOVE);
        //}
    }

    void UseRandomSkill()
    {
        ElapsedTime += Time.deltaTime;
        BasicAttackTime += Time.deltaTime;

        if (ElapsedTime >= SkillDelayTime)
        {
            if(Dragon.GetIsAniRunning() == false)
            {
                int RandomSkil = Random.Range((int)DRAGON_SKILL.BREATH, (int)DRAGON_SKILL.ENUM_END);
                Debug.Log("use dragonskill - " + RandomSkil);
                Dragon.PlayAnimation("Ani_State", (int)DRAGON_ANI_STATE.IDLE);
                Dragon.FsmUseDragonSkill(RandomSkil);
                ElapsedTime = 0.0f;
            }
        }
        else
        {
            if(BasicAttackTime >= BasicAttackDelayTime && Dragon.GetIsAniRunning() == false)
            {
                Dragon.PlayAnimation("Ani_State", (int)DRAGON_ANI_STATE.BITE_ATTACK);
                BasicAttackTime = 0.0f;
            }
        }
    }
}
