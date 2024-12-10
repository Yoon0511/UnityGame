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
        float value = Hp / MaxHp;
        HP_BAR.fillAmount = value;
    }
    void UpdateMpbar()
    {
        float value = Mp / MaxMp;
        MP_BAR.fillAmount = value;
    }
}
