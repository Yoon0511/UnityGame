using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPowerUp : Skill, ISkill
{
    public void UseSkill()
    {
        Debug.Log("Use PowerUp Skill");
    }
}
