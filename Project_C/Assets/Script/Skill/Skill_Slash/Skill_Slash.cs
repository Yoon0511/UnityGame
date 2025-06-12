using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Slash : Skill
{
    public GameObject SLASH_PREFAP;
    public override void UseSkill()
    {
        GameObject slash = Instantiate(SLASH_PREFAP);
        Player player = Owner.GetComponent<Player>();
        slash.GetComponent<Slash>().Init(player.transform.rotation, player.GetProjectilePoint().position, 
            Atk, 10.0f);
    }
}