using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_PowerUp : Skill
{
    public override void UseSkill()
    {
        StatBuff statBuff = new StatBuff(STAT_TYPE.ATK,5.0f,5.0f,Shared.GameMgr.PLAYEROBJ,SpriteName);

        Shared.GameMgr.PLAYER.BuffSystem.AddBuff(statBuff);
    }
}
