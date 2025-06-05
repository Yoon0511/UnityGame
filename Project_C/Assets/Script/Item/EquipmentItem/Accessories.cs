using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Accessories : EquipmentItem
{
    public override string GetEnhanceInfo()
    {
        string Stat = "<color=#FFA500>" + "SPEED " + "</color>" + "<color=#FFFFFF>" + DicEquipmentItemStat[STAT_TYPE.SPEED].ToString("F0") + "</color>";
        string Plus = "<color=#FFA500><b>  =>  </b></color>";
        string EnhanceStat = "<color=#00FF66><b>" + (DicEquipmentItemStat[STAT_TYPE.SPEED] + EnhanceRisingAmount).ToString("F0") + "</b></color>";
        string EnhaceProbability = "\n" + GetEnhanceProbabilityColor();
        string info = Stat + Plus + EnhanceStat + EnhaceProbability;
        return info;
    }
    public override bool Enhance()
    {
        if (TryEnhance())
        {
            EnhanceValue++;
            EnhanceProbability -= EnhanceProbabilityDecreaseAmount; //����Ȯ�� 5%�� ����
            DicEquipmentItemStat[STAT_TYPE.SPEED] += EnhanceRisingAmount; //EnhanceRisingAmount��ŭ�� ��������
            EnhanceMaterial += MaterialRisingAmount; //��ȭ�������
            return true;
        }
        else
        {
            return false;
        }
    }

    public override float GetItemStat()
    {
        return DicEquipmentItemStat[STAT_TYPE.SPEED];
    }

    public override string GetStrSTAT_Type()
    {
        return "SPEED";
    }

    public override string GetItemExplanation()
    {
        return $"<color=#FFFFFF>{GetStrSTAT_Type()}+</color> <color=#FF4500>{GetItemStat()}</color>";
    }
}
