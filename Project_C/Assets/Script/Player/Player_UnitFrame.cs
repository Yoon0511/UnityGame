using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player
{
    [SerializeField]
    UnitFrame UnitFrame;

    void UpdateUnitFrame()
    {
        UpdateHpbar();
        UpdateMpbar();
        UpdateExpbar();
    }

    void UpdateHpbar()
    {
        UnitFrame.UpdateHpbar(Statdata.GetData(STAT_TYPE.HP), Statdata.GetData(STAT_TYPE.MAXHP));
    }
    void UpdateMpbar()
    {
        UnitFrame.UpdateMpbar(Statdata.GetData(STAT_TYPE.MP), Statdata.GetData(STAT_TYPE.MAXMP));
    }
    void UpdateExpbar()
    {
        UnitFrame.UpdateExpbar(Statdata.GetData(STAT_TYPE.EXP), Statdata.GetData(STAT_TYPE.MAXEXP),Statdata.GetData(STAT_TYPE.LEVEL));
    }
}
