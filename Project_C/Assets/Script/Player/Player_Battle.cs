using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    IEnumerator AutomaticRecovery(float _delay)
    {
        while (true)
        {
            yield return new WaitForSeconds(_delay);

            // 초당 1씩 자동회복
            float RecoveryHP = Statdata.GetData(STAT_TYPE.MAXHP) * 0.01f;
            float RecoveryMP = Statdata.GetData(STAT_TYPE.MAXMP) * 0.01f;
            EnhanceStat(STAT_TYPE.HP, 1);
            EnhanceStat(STAT_TYPE.MP, 1);
            UpdateUnitFrame();
        }
    }

    public void Respwan()
    {
        ChangeState((int)STATE.IDLE);
        Shared.GameMgr.FadeInOut.SetFadeDuration(1.0f);
        Shared.GameMgr.FadeInOut.FadeIn();

        GameObject Effect = Shared.ParticleMgr.CreateParticle("SpwanEffect", transform, 3.0f);
        Effect.transform.SetParent(transform);

        transform.position = Shared.GameMgr.RespwanPoint.transform.position;

        EnhanceStat(STAT_TYPE.HP,GetInStatData(STAT_TYPE.MAXHP));
        EnhanceStat(STAT_TYPE.MP,GetInStatData(STAT_TYPE.MAXMP));
    }
}