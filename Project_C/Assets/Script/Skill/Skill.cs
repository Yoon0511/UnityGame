using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    public GameObject Owner; //스킬 사용자
    public string SpriteName;
    public float CoolTime;
    public string SkillName;
    public float Atk;
    public float UseMp;
    public string Explanation;
    public int SkillMotion;
    public int CurrentState;
    public SKILL_TYPE SkillType;
    public bool Instant; //true = 스킬모션 없이 즉시사용
    public bool IsAtkRange; //공격사거리표시를 사용 여부

    public void SetOwner(GameObject _owner)
    {
        Owner = _owner;
        CurrentState = (int)SKILL_STATE.READY;
    }
    public virtual void UseSkill()
    {
        CurrentState = (int)SKILL_STATE.RUNNING;
    }

    public virtual void SkillEnd()
    {
        CurrentState = (int)SKILL_STATE.END;
    }

    public int GetCurrentState() { return CurrentState; }
    public void SetCurrentState(int _state) {  CurrentState = _state; }

    public override bool Equals(object obj)
    {
        if(obj is Skill other)
        {
            return SkillName == other.SkillName;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return SkillName?.GetHashCode() ?? 0;
    }
}