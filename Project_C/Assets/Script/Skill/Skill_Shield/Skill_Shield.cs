using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Shield : Skill
{
    public float Duration;
    public float ShieldAmount;

    public override void UseSkill()
    {
        base.UseSkill();
        //사용자에게 쉴드 추가
        ShieldBuff ShieldBuff = new ShieldBuff(STAT_TYPE.SHIELD, ShieldAmount, Duration, Owner, "UI_Skill_Icon_Reflect");

        Shared.ParticleMgr.CreateParticle("MagicShieldBlue", Owner.transform, 4f);

        Owner.GetComponent<Character>().AddBuff(ShieldBuff);
    }
}
