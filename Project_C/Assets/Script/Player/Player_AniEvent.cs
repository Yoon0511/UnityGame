using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class Player
{
    [SerializeField]
    GameObject HITBOX;

    [SerializeField]
    GameObject SLASH_PARTICLE;
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
}
