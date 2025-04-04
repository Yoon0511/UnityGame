using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Dragon
{
    [SerializeField]
    AtkRange[] AttackRange;

    bool IsOnAttackRange = false;

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
        if(AttackRange[_skillindex].ACTIVE_CONTROL)
        {
            AttackRange[_skillindex].gameObject.SetActive(true);
            IsOnAttackRange = true;

            while (true)
            {
                AttackRange[_skillindex].StartSizeUp();
                if (AttackRange[_skillindex].IsStretchEnd())
                {
                    AttackRange[_skillindex].gameObject.SetActive(false);
                    IsOnAttackRange = false;
                    break;
                }
                yield return null;
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
