using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField]
    float GuardTime;

    bool IsGuard;

    void Guard()
    {
        IsGuard = true;
        PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.GUARD);
        Shared.ParticleMgr.CreateParticle("MagicShieldYellow", transform, GuardTime);
        StartCoroutine(IGuard());

    }

    IEnumerator IGuard()
    {
        yield return new WaitForSeconds(GuardTime);
        PlayAnimation("Ani_State",(int)PLAYER_ANI_STATE.IDLE);
        IsGuard = false;
    }

    void GuardSuccess()
    {
        Shared.ParticleMgr.CreateParticle("YellowHit", BodyParticlePoint.transform, 0.5f);
    }
}
