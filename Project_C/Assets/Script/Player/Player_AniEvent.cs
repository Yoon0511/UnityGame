using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class Player
{
    [SerializeField]
    GameObject HITBOX;

    [SerializeField]
    GameObject SLASH_PARTICLE;

    [SerializeField]
    GameObject SKILL_PARTICLE_POINT;
    public void OnHitBoxActive()
    {
        Shared.ParticleMgr.CreateParticle("Slash", SLASH_PARTICLE.transform, 0.2f);
        HITBOX.SetActive(true);
    }

    public void OffHitBoxActive()
    {
        HITBOX.SetActive(false);
    }

    public void OnAttackAniEnd()
    {
        //ChangeState(PrevState);
        ChangeState((int)STATE.IDLE);
    }

    public void OnCurrentUseSkill()
    {
        CurrentUseSkill();
    }

    public void OnSlashParticle()
    {
        Shared.ParticleMgr.CreateParticle("ChargeSlashRed", SKILL_PARTICLE_POINT.transform, 1.0f, gameObject.transform);
    }

    public override void OnAniEnd()
    {
        IsAniRunning = false;
        OnAttackAniEnd();
    }
}
