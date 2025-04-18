using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SelfHeal : Skill
{
    GameObject Target;
    public float HealAmount;

    public override void UseSkill()
    {
        Shared.ParticleMgr.CreateParticle("Healing",
            Owner.transform, 1f);

        Owner.GetComponent<Character>().EnhanceStat(STAT_TYPE.HP, HealAmount);
    }
}
