using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character
{
    [SerializeField]
    List<Skill> SkillList = new();
    [SerializeField]
    Transform ProjectilePoint;

    int CurrUseSkillIndex;
    public int MaxSkillCount = 3;
    [SerializeField]
    protected List<Skill> CurrentSkill = new();
    Dictionary<Skill, float> DicSkillCoolTime = new();

    bool IsCoolTimeRunning = false;

    public void AddSkill(Skill _skill)
    {
        SkillList.Add(_skill);
    }

    public abstract void UseSkill(int _index);
    protected void UseSkill(int _index,int _state,int _skillmotion)
    {
        if (_index < MaxSkillCount && CurrentSkill[_index] != null)
        {
            if (DicSkillCoolTime[CurrentSkill[_index]] <= 0f)
            {
                CurrUseSkillIndex = _index;
                ChangeState(_state, _skillmotion);

                DicSkillCoolTime[CurrentSkill[_index]] = CurrentSkill[_index].CoolTime;
                if (IsCoolTimeRunning == false)
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

    public void SetCurrentSkill(int _index, Skill _skill)
    {
        if (_index < MaxSkillCount)
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

            if (IsCoolTimeSkill == false)
            {
                IsCoolTimeRunning = false;
                yield break;
            }
            yield return null;
        }
    }
}
