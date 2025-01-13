using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player
{
    [SerializeField]
    UnitFrame UnitFrame;
    void UpdateHpbar()
    {
        UnitFrame.UpdateHpbar(Statdata.GetData(STAT_TYPE.HP), Statdata.GetData(STAT_TYPE.MAXHP));
    }
    void UpdateMpbar()
    {
        UnitFrame.UpdateMpbar(Statdata.GetData(STAT_TYPE.MP), Statdata.GetData(STAT_TYPE.MAXMP));
    }
}
