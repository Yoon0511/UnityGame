using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dragon_AttackState : StateBase
{
    Dragon Dragon;
    float SkillElapsedTime;
    float SkillDelayTime;
    float BiteAtkElapsedTime;
    float BiteAtkDelayTime;
    float SearchElapsedTime;
    float SearchDelayTime;
    bool Searching;
    public Dragon_AttackState(Dragon _dragon)
    {
        Dragon = _dragon;
    }
    public override void OnStateEnter()
    {
        SkillDelayTime = 7.0f;
        BiteAtkDelayTime = 1.5f;
        SearchDelayTime = 0.5f;
        BiteAtkElapsedTime = 1.2f;
        Searching = false;
    }

    public override void OnStateExit()
    {
        SkillElapsedTime = 0.0f;
        BiteAtkElapsedTime = 0.0f;
        SearchElapsedTime = 0.0f;
    }

    public override void OnStateUpdate()
    {
        UseRandomSkill();
    }

    void UseRandomSkill()
    {
        if(Searching)
        {
            SearchElapsedTime += Time.deltaTime;

            if (SearchElapsedTime >= SearchDelayTime)
            {
                SearchElapsedTime = 0.0f;

                if (Dragon.IsPlayerInAttackRange())
                {
                    Searching = false;
                }
                else
                {
                    Dragon.ChangeState((int)DRAGON_STATE.MOVE);
                }
            }
            return;
        }

        SkillElapsedTime += Time.deltaTime;

        if (SkillElapsedTime >= SkillDelayTime)
        {
            SkillElapsedTime = 0.0f;
            int RandomSkil = Random.Range((int)DRAGON_SKILL.BREATH, (int)DRAGON_SKILL.ENUM_END);

            //Dragon.UseDragonSkill(RandomSkil);
            //Dragon.UseDragonSkill((int)DRAGON_SKILL.BREATH); //0
            //Dragon.UseDragonSkill((int)DRAGON_SKILL.RUSH); //1
            //Dragon.UseDragonSkill((int)DRAGON_SKILL.FALLING_ROCK); //2
            //Dragon.UseDragonSkill((int)DRAGON_SKILL.ROAR); //3
            Dragon.UseDragonSkill(5); //3
            //Debug.Log(UseSkill.SkillName);

            BiteAtkElapsedTime = 0.0f;
        }
        else
        {
            BiteAtkElapsedTime += Time.deltaTime;

            if (Dragon.GetIsAniRunning() == false &&
                Dragon.GetIsOnAttackRange() == false &&
                Dragon.GetCurrentSkill().GetCurrentState() == (int)SKILL_STATE.READY &&
                BiteAtkElapsedTime >= BiteAtkDelayTime)
            {
                BiteAtkElapsedTime = 0.0f;

                Dragon.PlayAnimation("Ani_State", (int)DRAGON_ANI_STATE.BITE_ATTACK);
                
                Searching = true;
            }
        }
  
        if (Dragon.GetCurrentSkill().GetCurrentState() == (int)SKILL_STATE.END)
        {
            Dragon.GetCurrentSkill().SetCurrentState((int)SKILL_STATE.READY);
            
            if(Dragon.IsPlayerInAttackRange() == false)
            {
                Searching = true;
                Dragon.ChangeState((int)DRAGON_STATE.MOVE);
            }
        }
    }

    void Search()
    {
        if (Dragon.IsPlayerInAttackRange())
        {
            Searching = false;
        }
        else
        {
            Dragon.ChangeState((int)DRAGON_STATE.MOVE);
        }
    }
}
