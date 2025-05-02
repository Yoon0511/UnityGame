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

            //최대 HP,MP의 1%씩 자동회복
            float RecoveryHP = Statdata.GetData(STAT_TYPE.MAXHP) * 0.01f;
            float RecoveryMP = Statdata.GetData(STAT_TYPE.MAXMP) * 0.01f;
            EnhanceStat(STAT_TYPE.HP, RecoveryHP);
            EnhanceStat(STAT_TYPE.MP, RecoveryMP);
            UpdateUnitFrame();
        }
    }
}