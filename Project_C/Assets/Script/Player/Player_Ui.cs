using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public partial class Player
{
    [SerializeField]
    Image HP_BAR;
    [SerializeField]
    Image MP_BAR;
    void UpdateHpbar()
    {
        float value = Statdata.GetData(STAT_TYPE.HP) / Statdata.GetData(STAT_TYPE.MAXHP);
        HP_BAR.fillAmount = value;
    }
    void UpdateMpbar()
    {
        float value = Statdata.GetData(STAT_TYPE.MP) / Statdata.GetData(STAT_TYPE.MAXMP);
        MP_BAR.fillAmount = value;
    }
}
