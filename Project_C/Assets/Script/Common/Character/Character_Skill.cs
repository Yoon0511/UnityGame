using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract partial class Character
{
    [SerializeField]
    List<Skill> SkillList = new();
    [SerializeField]
    Transform ProjectilePoint;

    int CurrUseSkillIndex;
    public int MaxSkillCount = 4;
    [SerializeField]
    protected List<Skill> CurrentSkill = new List<Skill>();
    Dictionary<string, float> DicSkillCoolTime = new();

    bool IsCoolTimeRunning = false;
    int NoneSkillMotion = 0;
    public void AddSkill(Skill _skill)
    {
        SkillList.Add(_skill);
    }

    public abstract void UseSkill(int _index);
    protected void UseSkill(int _index,int _state,int _skillmotion, bool _instant = false)
    {
        if (_index < MaxSkillCount && CurrentSkill[_index] != null)
        {
            if (DicSkillCoolTime[CurrentSkill[_index].SkillName] <= 0f)
            {                
                CurrUseSkillIndex = _index;
                ChangeState(_state, _skillmotion);
                // MP소모
                Statdata.EnhanceStat(STAT_TYPE.MP, -CurrentSkill[CurrUseSkillIndex].UseMp);

                if (_skillmotion == NoneSkillMotion
                    || _instant == true)
                {
                    CurrentUseSkill();
                }

                DicSkillCoolTime[CurrentSkill[_index].SkillName] = CurrentSkill[_index].CoolTime;
                if (IsCoolTimeRunning == false)
                {
                    StartCoroutine((ICoolTime()));
                }
            }
        }
    }
    protected void UseSkill(int _index, int _skillmotion)
    {
        if (_index < MaxSkillCount && CurrentSkill[_index] != null)
        {
            if (DicSkillCoolTime[CurrentSkill[_index].SkillName] <= 0f)
            {
                CurrUseSkillIndex = _index;
                // MP소모
                Statdata.EnhanceStat(STAT_TYPE.MP, -CurrentSkill[CurrUseSkillIndex].UseMp);
                
                PlayAnimation("Ani_State", _skillmotion);

                if (_skillmotion == NoneSkillMotion)
                {
                    CurrentUseSkill();
                }

                DicSkillCoolTime[CurrentSkill[_index].SkillName] = CurrentSkill[_index].CoolTime;
                if (IsCoolTimeRunning == false)
                {
                    StartCoroutine((ICoolTime()));
                }
            }
        }
    }

    //스킬 모션은 나가지만 스킬타이밍은 즉발
    protected void UseSkill(int _index, int _skillmotion,bool _instant)
    {
        if (_index < MaxSkillCount && CurrentSkill[_index] != null)
        {
            if (DicSkillCoolTime[CurrentSkill[_index].SkillName] <= 0f)
            {
                CurrUseSkillIndex = _index;
                // MP소모
                Statdata.EnhanceStat(STAT_TYPE.MP, -CurrentSkill[CurrUseSkillIndex].UseMp);

                PlayAnimation("Ani_State", _skillmotion);

                if (_instant)
                {
                    CurrentUseSkill();
                }

                DicSkillCoolTime[CurrentSkill[_index].SkillName] = CurrentSkill[_index].CoolTime;
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
        //Statdata.EnhanceStat(STAT_TYPE.MP, -CurrentSkill[CurrUseSkillIndex].UseMp);
        
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
        return DicSkillCoolTime[CurrentSkill[_index].SkillName];
    }

    public float GetCurrentSkillCoolTime(int _index)
    {
        return CurrentSkill[_index].CoolTime;
    }

    public Skill GetCurrentSkill() { return CurrentSkill[CurrUseSkillIndex]; }
    public Skill GetCurrentSkill(int _index) { return CurrentSkill[_index]; }
    public void SetCurrentSkill(int _index, Skill _skill)
    {
        if (_index < 0 || _index >= MaxSkillCount || _skill == null)
            return;
        
        _skill.SetOwner(gameObject);
        CurrentSkill[_index] = _skill;
        
        if (!DicSkillCoolTime.ContainsKey(_skill.SkillName))
        {
            DicSkillCoolTime.Add(_skill.SkillName, 0f);
        }
        else
        {
            DicSkillCoolTime[CurrentSkill[_index].SkillName] = 0f;
        }
    }
    public void SetCurrentSkill(int _index, int _skillid)
    {
        if (_index < 0 || _index >= MaxSkillCount || _skillid == (int)PLAYER_SKILL_ID.NONE)
            return;

        //skillid로 스킬장착
        Skill _skill = null;
        for(int i = 0;i<SkillList.Count;++i)
        {
            if (SkillList[i].GetId() == _skillid)
            {
                _skill = SkillList[i];
                break;
            }
        }

        _skill.SetOwner(gameObject);
        CurrentSkill[_index] = _skill;

        if (!DicSkillCoolTime.ContainsKey(_skill.SkillName))
        {
            DicSkillCoolTime.Add(_skill.SkillName, 0f);
        }
        else
        {
            DicSkillCoolTime[CurrentSkill[_index].SkillName] = 0f;
        }
    }
    public void SwapSkill(int _skillindex1, int _skillindex2)
    {
        Skill temp = CurrentSkill[_skillindex1];
        CurrentSkill[_skillindex1] = CurrentSkill[_skillindex2];
        CurrentSkill[_skillindex2] = temp;
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

                if (DicSkillCoolTime[skill.SkillName] > 0f)
                {
                    DicSkillCoolTime[skill.SkillName] -= Time.deltaTime;
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
