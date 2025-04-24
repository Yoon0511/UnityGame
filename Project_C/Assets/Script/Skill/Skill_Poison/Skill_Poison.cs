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

        //이펙트 생성
        Shared.ParticleMgr.CreateParticle("SparksExplodePurple",
            Target.GetBodyParticlePointObj().transform, 0.7f);

        //도트데미지 생성 및 적용
        GameObject obj = Target.gameObject;
        DotDamage dotDamage = new DotDamage(DotInterval, STAT_TYPE.HP, Atk, Duration, obj, "UI_Skill_Icon_PsycicAttack");
        Target.AddBuff(dotDamage);
    }
}