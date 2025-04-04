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
    float TargetSearchTime;
    float TargetSearchDelayTime;

    public Dragon_AttackState(Dragon _dragon)
    {
        Dragon = _dragon;
    }
    public override void OnStateEnter()
    {
        SkillDelayTime = 15.0f;
        BasicAttackDelayTime = 2.0f;
        TargetSearchTime = 0.5f;

        Dragon.PlayAnimation("Ani_State", (int)DRAGON_ANI_STATE.IDLE);

        Dragon.ChangeAnimationWaitForSecond("Ani_State", (int)DRAGON_ANI_STATE.BITE_ATTACK,0.5f);
    }

    public override void OnStateExit()
    {
        ElapsedTime = 0;
        BasicAttackTime = 0;
        TargetSearchDelayTime = 0;
    }

    public override void OnStateUpdate()
    {
        UseRandomSkill();

        CheckAttackRange();
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

                Dragon.UseDragonSkill(RandomSkil);
                //Dragon.UseDragonSkill((int)DRAGON_SKILL.RUSH);
                ElapsedTime = 0.0f;
                TargetSearchTime = 0.0f;
            }
        }
        else
        {
            if(BasicAttackTime >= BasicAttackDelayTime && Dragon.GetIsAniRunning() == false 
                && Dragon.GetIsOnAttackRange() == false)
            {
                Dragon.PlayAnimation("Ani_State", (int)DRAGON_ANI_STATE.BITE_ATTACK);
                BasicAttackTime = 0.0f;
            }
        }
    }

    void CheckAttackRange()
    {
        if (Dragon.GetCurrentSkill().GetCurrentState() == (int)SKILL_STATE.END)
        {
            TargetSearchDelayTime += Time.deltaTime;
            if (TargetSearchDelayTime >= TargetSearchTime)
            {
                TargetSearchDelayTime = 0.0f;

                if (Dragon.IsPlayerInAttackRange() == false)
                {
                    Dragon.ChangeState((int)DRAGON_STATE.MOVE);
                }
            }
        }
    }

    //스킬사용후 탐색시간

    //스킬사용 종료후 -> 일정시간(0.3s) 정지하여 어그로 탐색 -> 평타 및 스킬 시전
    //                                     or -> 공격범위를 벗어나 있으면 다가감 -> 평타 및 스킬시전
}
