using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class Player : Character
{
    [SerializeField]
    GameObject HITBOX;

    [SerializeField]
    GameObject SLASH_TRAIL;
    public void OnHitBoxActive()
    {
        //Shared.ParticleMgr.CreateParticle("Slash", SLASH_PARTICLE.transform, 0.2f);
        HITBOX.SetActive(true);
    }

    public void OffHitBoxActive()
    {
        HITBOX.SetActive(false);
    }

    public void OnAttackAniEnd()
    {
        if(IsRiding == false && IsAutoMode == false)
        {
            ChangeState((int)STATE.IDLE);
        }
    }

    public void OnCurrentUseSkill()
    {
        CurrentUseSkill();
    }

    public void OnSlashParticle()
    {
        Shared.ParticleMgr.CreateParticle("ChargeSlashBlack", BodyParticlePoint.transform, 1.0f, gameObject.transform);
    }

    public override void OnAniEnd()
    {
        IsAniRunning = false;
        OnAttackAniEnd();
    }

    public void OnComboEnable()
    {
        IsComboEnable = true;
    }

    public void OnComboDisable()
    {
        IsComboEnable = false;
    }

    public void OnComboExit()
    {
        SLASH_TRAIL.SetActive(false);
        ComboIndex = 0;
        IsComboEnable = false;
        IsBasicAttack = true;
        SetAnimatorBool("Ani_IsSlashCombo", false);
        OnAttackAniEnd();
    }

    public void OnBasicAttak()
    {
        IsBasicAttack = false;
        SLASH_TRAIL.SetActive(true);
    }
}
