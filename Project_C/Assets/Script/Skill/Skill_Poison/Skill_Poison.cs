using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Skill_Poison : Skill
{
    public float Duration;
    public float DotInterval;
    Character Target;
    public override void UseSkill()
    {
        Target = Owner.GetComponent<Character>().GetTargetCharacter();
        if (Target == null)
            return;

        GameObject obj = Target.gameObject;
        DotDamage dotDamage = new DotDamage(DotInterval, STAT_TYPE.HP, Atk, Duration, obj, "UI_Skill_Icon_PsycicAttack");
        Target.AddBuff(dotDamage);
    }
}