using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : EquipmentItem
{
    public override string GetEnhanceInfo()
    {
        string Stat = "<color=#FFA500>" + "ATK " + "</color>" + "<color=#FFFFFF>" + DicEquipmentItemStat[STAT_TYPE.ATK].ToString("F0") + "</color>";
        string Plus = "<color=#FFA500><b>  =>  </b></color>";
        string EnhanceStat = "<color=#00FF66><b>" + (DicEquipmentItemStat[STAT_TYPE.ATK] + EnhanceRisingAmount).ToString("F0") + "</b></color>";
        string EnhaceProbability = "\n" + GetEnhanceProbabilityColor();
        string info = Stat + Plus + EnhanceStat + EnhaceProbability;
        return info;
    }
    public override bool Enhance()
    {
        if(TryEnhance())
        {
            Debug.Log(EnhanceValue.ToString() + ItemName + "Success");
            EnhanceValue++;
            EnhanceProbability -= 0.05f;
            DicEquipmentItemStat[STAT_TYPE.ATK] += EnhanceRisingAmount;

            return true;
        }
        else
        {
            Debug.Log(ItemName + "fail");
            return false;
        }
    }

    public override float GetItemStat()
    {
        return DicEquipmentItemStat[STAT_TYPE.ATK];
    }

    public override string GetStrSTAT_Type()
    {
        return "ATK";
    }
}
