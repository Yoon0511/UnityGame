using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Slash : Skill
{
    public GameObject SLASH_PREFAP;
    public override void UseSkill()
    {
        GameObject slash = Instantiate(SLASH_PREFAP);
        slash.GetComponent<Slash>().Init(Shared.GameMgr.PLAYER.transform.rotation,Shared.GameMgr.PLAYER.GetProjectilePoint().position, 
            Atk, 10.0f);
    }
}