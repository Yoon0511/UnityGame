using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    public GameObject Owner;
    public string SpriteName;
    public float CoolTime;
    public string SkillName;
    public float Atk;
    public float UseMp;
    public string Explanation;
    public int SkillMotion;
    public int SkillUseState;
    public SKILL_TYPE SkillType;

    public void SetOwner(GameObject _owner)
    {
        Owner = _owner;
    }
    public virtual void UseSkill()
    {

    }

    public virtual void SkillEnd()
    {

    }
}