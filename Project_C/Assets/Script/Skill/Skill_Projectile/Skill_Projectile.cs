using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Projectile : Skill
{
    public GameObject ProjectileObj;
    
    public override void UseSkill()
    {
        base.UseSkill();
        GameObject target = Owner.GetComponent<Character>().GetTargetCharacter().GetBodyParticlePointObj();

        GameObject obj = Instantiate(ProjectileObj);
        obj.transform.position = OwnerCharacter.GetProjectilePoint().position;
        ProjectileObjBase projectileObjBase = obj.GetComponent<ProjectileObjBase>();
        projectileObjBase.Init(Atk,target);
        base.SkillEnd();
    }

    
}
