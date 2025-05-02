using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Dragon_AttackState : StateBase
{
    Dragon Dragon;
    float SkillElapsedTime;
    float SkillDelayTime;       // ��ų ��� �ֱ�
    float BiteAtkElapsedTime;
    float BiteAtkDelayTime;     // ��Ÿ ��� �ֱ�
    float SearchElapsedTime;
    float SearchDelayTime;      // Ž���ϴ� ���� ���ߴ� �ð�
    bool Searching;

    float SequenceElapsedTime;
    float SequenceDelayTime;    //�߰� ��ų ���� ���


    int CurrSkill = 0;
    public Dragon_AttackState(Dragon _dragon)
    {
        Dragon = _dragon;
    }
    public override void OnStateEnter()
    {
        SkillDelayTime = 6.0f;
        BiteAtkDelayTime = 1.5f;
        SearchDelayTime = 0.5f;
        BiteAtkElapsedTime = 1.2f;
        SkillElapsedTime = 5.0f;
        Searching = false;

        SequenceDelayTime = 0.7f;
        SequenceElapsedTime = 0f;
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
        

        if (Dragon.GetIsAniRunning() == false &&
               Dragon.GetIsOnAttackRange() == false &&
               Dragon.GetCurrentSkill().GetCurrentState() == (int)SKILL_STATE.READY &&
            SkillElapsedTime >= SkillDelayTime)
        {
            SkillElapsedTime = 0.0f;
            int RandomSkil = Random.Range((int)DRAGON_SKILL.BREATH, (int)DRAGON_SKILL.ENUM_END);

            // Ÿ���� ���� ȸ��
            Dragon.LookatTarget();
            //Dragon.UseDragonSkill((int)DRAGON_SKILL.RUSH);
            CurrSkill = (int)DRAGON_SKILL.ROAR;
            Debug.Log("Atk");
            Dragon.UseDragonSkill(CurrSkill);
        }

            //if (SkillElapsedTime >= SkillDelayTime)
            //{
            //    SkillElapsedTime = 0.0f;
            //    int RandomSkil = Random.Range((int)DRAGON_SKILL.BREATH, (int)//DRAGON_SKILL.ENUM_END);
            //
            //    //Dragon.UseDragonSkill(RandomSkil);
            //    //Dragon.UseDragonSkill((int)DRAGON_SKILL.BREATH); //0
            //    //Dragon.UseDragonSkill((int)DRAGON_SKILL.RUSH); //1
            //    //Dragon.UseDragonSkill((int)DRAGON_SKILL.FALLING_ROCK); //2
            //    //Dragon.UseDragonSkill((int)DRAGON_SKILL.ROAR); //3
            //    //Dragon.UseDragonSkill(5); //3
            //    //Debug.Log(UseSkill.SkillName);
            //
            //    BiteAtkElapsedTime = 0.0f;
            //}
            //else
            //{
            //    BiteAtkElapsedTime += Time.deltaTime;
            //
            //    if (Dragon.GetIsAniRunning() == false &&
            //        Dragon.GetIsOnAttackRange() == false &&
            //        Dragon.GetCurrentSkill().GetCurrentState() == (int)//SKILL_STATE.READY &&
            //        BiteAtkElapsedTime >= BiteAtkDelayTime)
            //    {
            //        BiteAtkElapsedTime = 0.0f;
            //
            //        Dragon.PlayAnimation("Ani_State", (int)//DRAGON_ANI_STATE.BITE_ATTACK);
            //        
            //        Searching = true;
            //    }
            //}

        if (Dragon.GetCurrentSkill().GetCurrentState() == (int)SKILL_STATE.END && Dragon.GetIsAniRunning() == false)
        {
            Dragon.GetCurrentSkill().SetCurrentState((int)SKILL_STATE.READY);

            // �߰� ��ų ����
            while(SequenceElapsedTime <= SequenceDelayTime)
            {
                SequenceElapsedTime += Time.deltaTime;
            }
            if (SequenceElapsedTime >= SequenceDelayTime)
            {
                SequenceElapsedTime = 0.0f;

                SequenceAtk(CurrSkill);

                if (Dragon.IsPlayerInAttackRange() == false)
                {
                    Searching = true;
                    Dragon.ChangeState((int)DRAGON_STATE.MOVE);
                }
            }
        }
    }

    void SequenceAtk(int _CurrSkill)
    {
        switch (_CurrSkill)
        {
            case (int)DRAGON_SKILL.ROAR:
                {
                    Skill_Roar Roar = Dragon.GetCurrentSkill() as Skill_Roar;
                    if (Roar != null && Roar.IsHit)
                    {
                        //�ξ� ��ų ���߽� 70% Ȯ���� Brath �߰� ����
                        if(ShouldPerformExtraAttack(0.7f))
                        {
                            SkillElapsedTime = 0.0f;
                            _CurrSkill = (int)DRAGON_SKILL.BREATH;
                            Dragon.LookatTarget();
                            Dragon.UseDragonSkill(_CurrSkill);
                        }
                    }
                }
            break;
        }
    }

    bool ShouldPerformExtraAttack(float _probability)
    {
        return Random.value < _probability;
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
