using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField]
    List<Skill> SkillList = new List<Skill>();
    [SerializeField]
    SkillBook SkillBook;

    Skill[] CurrentSkill = new Skill[3];
    public void AddSkill(Skill _skill)
    {
        SkillList.Add(_skill);
    }

    public void UseSkill(int _index)
    {
        if (_index < CurrentSkill.Length)
        {
            CurrentSkill[_index].UseSkill();
        }
    }

    public List<Skill> GetSkillList()
    {
        return SkillList;
    }

    public void SetCurrentSkill(int _index,Skill _skill)
    {
        if (_index < CurrentSkill.Length)
        {
            CurrentSkill[_index] = _skill;
        }
    }
}
