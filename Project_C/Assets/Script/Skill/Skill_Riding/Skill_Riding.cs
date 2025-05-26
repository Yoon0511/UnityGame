using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Riding : Skill
{
    public bool IsRiding = false;
    public override void UseSkill()
    {
        base.UseSkill();
        if(IsRiding == false) //≈ª∞Õ º“»Ø
        {
            Owner.GetComponent<Player>().OnRiding();
            IsRiding = true;
        }
        else //≈ª∞Õ «ÿ¡¶
        {
            Owner.GetComponent<Player>().OffRiding();
            IsRiding = false;
        }
        base.SkillEnd();
    }
}
