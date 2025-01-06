using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    public Sprite Sprite;
    public float CoolTime;
    public string SkillName;
    public float Atk;
    public float UseMp;
    public string Explanation;
    public int SkillMotion;
    public SKILL_TYPE SkillType;

    public virtual void UseSkill()
    {

    }
}