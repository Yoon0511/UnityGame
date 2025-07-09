using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Dragon
{
    [SerializeField]
    AtkRange[] AtkRange;

    bool IsOnAttackRange = false;
    public int SkillCount = 0;
    void SkillInit()
    {
        foreach (var skill in GetSkillList())
        {
            CurrentSkill.Add(null);
        }

        int i = 0;
        foreach (var skill in GetSkillList())
        {
            SetCurrentSkill(i, skill);
            i++;
        }
    }
    public IEnumerator IUseDragonSkill(int _skillindex)
    {
        if(CurrentSkill[_skillindex].IsAtkRange) //스킬에 공격사거리표시가 있을 경우 실행
        {
            if (AtkRange[_skillindex].ACTIVE_CONTROL) //공격사거리표시가 끝나야 발동되는 스킬
            {
                AtkRange[_skillindex].gameObject.SetActive(true);
                IsOnAttackRange = true;

                while (true)
                {
                    AtkRange[_skillindex].StartSizeUp();
                    if (AtkRange[_skillindex].IsStretchEnd())
                    {
                        AtkRange[_skillindex].gameObject.SetActive(false);
                        IsOnAttackRange = false;
                        break;
                    }
                    yield return null;
                }
            }
        }
       
        //switch(_skillindex)
        //{
        //    case (int)DRAGON_SKILL.BREATH:
        //        //UseSkill(_skillindex, (int)DRAGON_STATE.ATTACK, (int)DRAGON_ANI_STATE.BREATH);
        //        UseSkill(_skillindex, CurrentSkill[_skillindex].SkillMotion);
        //        break;
        //    case (int)DRAGON_SKILL.RUSH:
        //        //UseSkill(_skillindex, (int)DRAGON_STATE.ATTACK, (int)DRAGON_ANI_STATE.FORWARD_MOVE);
        //        UseSkill(_skillindex, (int)DRAGON_ANI_STATE.FORWARD_MOVE);
        //        break;
        //}

        UseSkill(_skillindex, CurrentSkill[_skillindex].SkillMotion);
    }

    public bool GetIsOnAttackRange() { return  IsOnAttackRange; }
}
