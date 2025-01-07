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
    Dictionary<Skill,float> DicSkillCoolTime = new Dictionary<Skill,float>();

    bool IsCoolTimeRunning = false;

    public void AddSkill(Skill _skill)
    {
        SkillList.Add(_skill);
    }

    public void UseSkill(int _index)
    {
        if (_index < CurrentSkill.Length && CurrentSkill[_index] != null)
        {
            if (DicSkillCoolTime[CurrentSkill[_index]] <= 0f)
            {
                CurrentSkill[_index].UseSkill();

                DicSkillCoolTime[CurrentSkill[_index]] = CurrentSkill[_index].CoolTime;
                if(IsCoolTimeRunning == false)
                {
                    StartCoroutine((ICoolTime()));
                }
            }
        }
    }

    public List<Skill> GetSkillList()
    {
        return SkillList;
    }

    public bool IsCurrentSkillNull(int _index)
    {
        return CurrentSkill[_index] == null ? true : false;
    }
    public float GetSkillCoolTime(int _index)
    {
        return DicSkillCoolTime[CurrentSkill[_index]];
    }

    public float GetCurrentSkillCoolTime(int _index)
    {
        return CurrentSkill[_index].CoolTime;
    }

    public void SetCurrentSkill(int _index,Skill _skill)
    {
        if (_index < CurrentSkill.Length)
        {
            if(CurrentSkill[_index] != null && 
                DicSkillCoolTime.ContainsKey(CurrentSkill[_index]))
            {
                DicSkillCoolTime.Remove(CurrentSkill[_index]);
            }

            CurrentSkill[_index] = _skill;

            //다른 스킬 칸에 같은 스킬이 있을 경우
            DicSkillCoolTime.Add(CurrentSkill[_index], 0f);
        }
    }

    IEnumerator ICoolTime()
    {
        IsCoolTimeRunning = true;
        while (true)
        {
            bool IsCoolTimeSkill = false;
            foreach (Skill skill in CurrentSkill)
            {
                if (skill == null) 
                    continue;

                if (DicSkillCoolTime[skill] > 0f)
                {
                    DicSkillCoolTime[skill] -= Time.deltaTime;
                    IsCoolTimeSkill = true;
                }
            }

            if(IsCoolTimeSkill == false)
            {
                IsCoolTimeRunning = false;
                yield break;
            }
            yield return null; 
        }
    }
}

//빈 슬롯
//스킬이 있는 슬롯