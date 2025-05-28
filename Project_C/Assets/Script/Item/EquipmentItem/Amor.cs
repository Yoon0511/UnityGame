using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amor : EquipmentItem
{
    public override string GetEnhanceInfo()
    {
        string Stat = "<color=#FFA500>" + "DEF " + "</color>"+"<color=#FFFFFF>"+ DicEquipmentItemStat[STAT_TYPE.DEF].ToString("F0") + "</color>";
        string Plus = "<color=#FFA500><b>  =>  </b></color>";
        string EnhanceStat = "<color=#00FF66><b>" + (DicEquipmentItemStat[STAT_TYPE.DEF] + EnhanceRisingAmount).ToString("F0") + "</b></color>";
        string EnhaceProbability = "\n" + GetEnhanceProbabilityColor();
        string info = Stat + Plus + EnhanceStat + EnhaceProbability;
        return info;
    }
    public override bool Enhance()
    {
        if (TryEnhance())
        {
            EnhanceValue++;
            EnhanceProbability -= EnhanceProbabilityDecreaseAmount;
            DicEquipmentItemStat[STAT_TYPE.DEF] += EnhanceRisingAmount;
            EnhanceMaterial += MaterialRisingAmount; //강화 비용 증가

            return true;
        }
        else
        {
            return false;
        }
    }

    public override float GetItemStat()
    {
        return DicEquipmentItemStat[STAT_TYPE.DEF];
    }

    public override string GetStrSTAT_Type()
    {
        return "DEF";
    }
}
