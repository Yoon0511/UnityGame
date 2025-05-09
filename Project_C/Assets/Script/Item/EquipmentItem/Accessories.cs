using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessories : EquipmentItem
{
    private void Start()
    {
        EnhanceValue = 0;
        EnhanceRisingAmount = 0.5f;
    }
    public override string EnhanceInfo()
    {
        string Stat = "<color=#CCCCCC>" + DicEquipmentItemStat[STAT_TYPE.SPEED].ToString("F0") + "</color>";
        string Plus = "<color=#FFA500><b>  =>  </b></color>";
        string EnhanceStat = "<color=#00FF66><b>" + (DicEquipmentItemStat[STAT_TYPE.SPEED] + EnhanceRisingAmount).ToString("F0") + "</b></color>";
        string info = Stat + Plus + EnhanceStat;
        return info;
    }
    public override bool Enhance()
    {
        if (TryEnhance())
        {
            EnhanceValue++;
            EnhanceProbability -= 0.05f;
            DicEquipmentItemStat[STAT_TYPE.SPEED] += EnhanceRisingAmount;

            return true;
        }
        else
        {
            return false;
        }
    }
}
