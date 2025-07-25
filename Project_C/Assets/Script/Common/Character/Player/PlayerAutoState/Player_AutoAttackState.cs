using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AutoAttackState : StateBase
{
    Player Player;
    float AttackCoolTime;
    float ATtackElpsedTime;
    float AutoUseSkillCoolTime;
    float AutoUseSkillElpsedTime;
    bool UseSkill;
    bool TrySkill;
    public Player_AutoAttackState(Player _player)
    {
        Player = _player;
    }

    public override void OnStateEnter()
    {
        AutoUseSkillCoolTime = 2.0f;
        AutoUseSkillElpsedTime = 0.0f;
        UseSkill = false;
        TrySkill = true;
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        if (Player.GetTargetCharacter().GetIsDead())
        {
            Player.OnComboExit();
            Player.ChangeState((int)AUTO_STATE.CHASE);
            return;
        }

        if (TrySkill)
        {
            for (int i = 0; i < 3; ++i)
            {
                var skill = Player.GetCurrentSkill(i);
                if (skill != null && skill.GetCurrentState() == (int)SKILL_STATE.READY)
                {
                    UseSkill = true;

                    // ��Ÿ ��� ���̸� ĵ���ϰ� ��ų ���
                    if (Player.GetAnimator().GetBool("Ani_IsSlashCombo") == true)
                    {
                        Player.SetAnimatorBool("Ani_IsSlashCombo", false);
                    }

                    Shared.UiMgr.SkillBtn[i].UseSkill(); // ��ų ���
                    TrySkill = false;
                    break;
                }
                else
                {
                    UseSkill = false;
                }
            }
        }

        if (UseSkill)
        {
            AutoUseSkillElpsedTime += Time.deltaTime;

            if (AutoUseSkillElpsedTime >= AutoUseSkillCoolTime)
            {
                // ��Ÿ ��� ���̸� ĵ���ϰ� ��ų ���
                if (Player.GetAnimator().GetBool("Ani_IsSlashCombo") == true)
                {
                    Player.SetAnimatorBool("Ani_IsSlashCombo", false);
                }

                AutoUseSkillElpsedTime = 0.0f;
                TrySkill = true;
                UseSkill = false;
            }
        }
        else
        {
            Player.AutoAttack(); // ��Ÿ ����
        }
    }
}
