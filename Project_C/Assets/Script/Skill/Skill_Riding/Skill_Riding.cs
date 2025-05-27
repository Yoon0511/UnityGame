using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Skill_Riding : Skill
{
    public bool IsRiding = false;
    private void Start()
    {
        IsRiding = false;
    }
    public override void UseSkill()
    {
        base.UseSkill();

        if (IsRiding == false) //Ż�� ��ȯ
        {
            Owner.GetComponent<Player>().OnRiding();
            IsRiding = true;
        }
        else //Ż�� ����
        {
            Owner.GetComponent<Player>().OffRiding();
            IsRiding = false;
        }
        base.SkillEnd();
    }
}
