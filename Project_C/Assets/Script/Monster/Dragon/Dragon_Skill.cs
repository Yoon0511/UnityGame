using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Dragon
{
    [SerializeField]
    AtkRange AttackRange_Cone;

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
    public IEnumerator Dragon_Braeth()
    {
        AttackRange_Cone.gameObject.SetActive(true);
        while (true)
        {
            AttackRange_Cone.StartSizeUp();
            if (AttackRange_Cone.IsStretchEnd())
            {
                AttackRange_Cone.gameObject.SetActive(false);
                break;
            }
            yield return null;
        }

        UseSkill(0, (int)DRAGON_STATE.ATTACK, (int)DRAGON_ANI_STATE.BREATH);
    }
}
