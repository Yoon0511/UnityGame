using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PowerUp : Skill
{
    public STAT_TYPE StatType;
    public float Duration;
    public float StatIncrease;
    public override void UseSkill()
    {
        Player character = Owner.GetComponent<Player>();
        if(character.GetPVIsMine())
        {
            StatBuff statBuff = new StatBuff(StatType, StatIncrease, Duration, Owner,SpriteName);
            character.AddBuff(statBuff);
        }
        
        Shared.ParticleMgr.CreateParticle("Buff", character.transform, 1.5f);
    }
}
