using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField]
    List<Skill> SkillList = new List<Skill>();
    [SerializeField]
    SkillBook SkillBook;
    [SerializeField]
    Transform ProjectilePoint;

    int CurrUseSkillIndex;
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
                CurrUseSkillIndex = _index;
                int PlayerSkillAnimation = (int)CurrentSkill[CurrUseSkillIndex].SkillMotion;
                //CurrentSkill[CurrUseSkillIndex].UseSkill();
                ChangeState(STATE.ATTACK, PlayerSkillAnimation);

                DicSkillCoolTime[CurrentSkill[_index]] = CurrentSkill[_index].CoolTime;
                if(IsCoolTimeRunning == false)
                {
                    StartCoroutine((ICoolTime()));
                }
            }
        }
    }

    public Transform GetProjectilePoint()
    {
        return ProjectilePoint;
    }
    public void CurrentUseSkill()
    {
        Statdata.EnhanceStat(STAT_TYPE.MP, -CurrentSkill[CurrUseSkillIndex].UseMp);
        UpdateMpbar();
        CurrentSkill[CurrUseSkillIndex].UseSkill();
    }
    public List<Skill> GetSkillList()
    {
        return SkillList;
    }

    public bool IsCurrentSkillNull(int _index)
    {
        return CurrentSkill[_index] == null;
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
            if (CurrentSkill[_index] != null &&
                DicSkillCoolTime.ContainsKey(CurrentSkill[_index]))
            {
                DicSkillCoolTime.Remove(CurrentSkill[_index]);
            }
            CurrentSkill[_index] = _skill;
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