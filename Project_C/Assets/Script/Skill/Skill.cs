using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill : MonoBehaviour
{
    public Sprite SPRITE;
    public float COOLTIME;
    public string SKILLNAME;
    public float ATK;
    public float USE_MP;
    public string EXPLANATION;
    public int ANIMATION_MOTION;
    public SKILL_TYPE SKILLTYPE;
    public abstract void UseSkill();
}
