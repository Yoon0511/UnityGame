using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillSlash : Skill, ISkill
{
    public void UseSkill()
    {
        Debug.Log("Use Slash Skill");
    }
}