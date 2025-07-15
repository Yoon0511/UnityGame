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
        AutoUseSkillCoolTime = 1.0f;
        AutoUseSkillElpsedTime = 0.0f;
        UseSkill = false;
        TrySkill = true;
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        if(TrySkill)
        {
            for (int i = 0; i < 3; ++i)
            {
                if (Player.GetCurrentSkill(i) != null)
                {
                    if (Player.GetCurrentSkill(i).GetCurrentState() == (int)SKILL_STATE.READY)
                    {
                        UseSkill = true;

                        if (Player.GetCurrAnimation() == (int)PLAYER_ANI_STATE.IDLE ||
                            Player.GetCurrAnimation() == (int)PLAYER_ANI_STATE.WALK ||
                            Player.GetCurrAnimation() == (int)PLAYER_ANI_STATE.RUN)
                        {
                            Shared.UiMgr.SkillBtn[i].UseSkill();
                            TrySkill = false;
                        }
                        break;
                    }
                    else
                    {
                        UseSkill = false;
                    }
                }
            }
        }

        if (UseSkill)
        {
            AutoUseSkillElpsedTime += Time.deltaTime;
            if(AutoUseSkillElpsedTime >= AutoUseSkillCoolTime)
            {
                AutoUseSkillElpsedTime = 0.0f;
                TrySkill = true;
            }
        }
        else
        {
            Player.AutoAttack();
            UseSkill = false;
        }

        if (Player.GetTargetCharacter().GetIsDead())
        {
            Player.ChangeState((int)AUTO_STATE.CHASE);
        }
    }
}
