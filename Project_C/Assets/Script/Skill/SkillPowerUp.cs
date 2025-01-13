using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPowerUp : Skill
{
    public override void UseSkill()
    {
        Debug.Log("Use PowerUp Skill");
        StatBuff statBuff = new StatBuff(STAT_TYPE.ATK,5.0f,5.0f,Shared.GameMgr.PLAYEROBJ);
        Shared.GameMgr.PLAYER.BuffMgr.AddBuff(statBuff);
    }
}
