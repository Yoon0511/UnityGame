using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Postion : ItemBase
{
    public float HealAmount = 100f;
    public override void ItemUse()
    {
        Owner.EnhanceStat(STAT_TYPE.HP, HealAmount);
        Shared.ParticleMgr.CreateParticle("Healing", Owner.GetBodyParticlePointObj().transform, 1.0f);
    }
}
