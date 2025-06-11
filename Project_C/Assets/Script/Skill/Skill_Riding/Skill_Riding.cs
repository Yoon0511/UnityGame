using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Skill_Riding : Skill
{
    public override void UseSkill()
    {
        base.UseSkill();

        bool IsRiding = Owner.GetComponent<Player>().GetIsRiding();
        if (IsRiding == false) //≈ª∞Õ º“»Ø
        {
            Owner.GetComponent<Player>().OnRiding();
        }
        else //≈ª∞Õ «ÿ¡¶
        {
            Owner.GetComponent<Player>().OffRiding();
        }
        base.SkillEnd();
    }
}
