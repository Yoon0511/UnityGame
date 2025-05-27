using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Riding_Run : StateBase
{
    Riding Riding;
    float DustCoolTime;
    float DustElpsedTime;
    public Riding_Run(Riding _riding)
    {
        DustCoolTime = 0.2f;
        Riding = _riding;
    }
    public override void OnStateEnter()
    {
        Riding.GetOwner().Statdata.SetStat(STAT_TYPE.SPEED, Riding.GetRunSpeed());
        Riding.PlayAnimation((int)RIDING_STATE.RUN);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        DustElpsedTime += Time.deltaTime;
        if(DustElpsedTime >= DustCoolTime)
        {
            DustElpsedTime = 0.0f;
            Shared.ParticleMgr.CreateParticle("Dust", Riding.BACKFOOT_L, 0.3f);
            Shared.ParticleMgr.CreateParticle("Dust", Riding.BACKFOOT_R, 0.3f);
            Shared.ParticleMgr.CreateParticle("Dust", Riding.FRONTFOOT_R, 0.2f);
            Shared.ParticleMgr.CreateParticle("Dust", Riding.FRONTFOOT_L, 0.2f);
        }
    }
}
