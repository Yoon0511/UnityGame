using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPoison : Skill, ISkill
{
    public void UseSkill()
    {
        Debug.Log("Use Skill Poison");
    }
}