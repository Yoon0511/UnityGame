using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
    [SerializeField]
    float GuardTime;

    bool IsGuard;

    void Guard()
    {
        Shared.ParticleMgr.CreateParticle("MagicShieldYellow", transform, GuardTime);
        
        if(PV.IsMine == false)
        {
            return;
        }

        IsGuard = true;
        PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.GUARD);
        StartCoroutine(IGuard());
    }

    IEnumerator IGuard()
    {
        yield return new WaitForSeconds(GuardTime);
        PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.IDLE);
        IsGuard = false;
    }

    void GuardSuccess()
    {
        Shared.ParticleMgr.CreateParticle("YellowHit", BodyParticlePoint.transform, 0.5f);
    }
}
