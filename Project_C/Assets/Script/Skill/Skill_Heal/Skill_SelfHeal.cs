using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_SelfHeal : Skill
{
    GameObject Target;
    public override void UseSkill()
    {
        Shared.ParticleMgr.CreateParticle("Healing",
            Owner.transform, 1f);
    }
}
