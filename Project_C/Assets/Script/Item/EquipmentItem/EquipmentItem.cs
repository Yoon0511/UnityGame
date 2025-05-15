using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
using static UnityEngine.Rendering.PostProcessing.HistogramMonitor;
public abstract class EquipmentItem : ItemBase
{
    public EQUITMENT_TYPE EquimentType;
    public SerializedDictionary<STAT_TYPE, float> DicEquipmentItemStat;

    public int EnhanceValue;
    public float EnhanceRisingAmount;
    protected float EnhanceProbability = 1.0f;
    protected int EnhanceMaterial;
    protected int MaterialRisingAmount;

    public EquipmentItem()
    {
        EnhanceMaterial = 100;
    }
    public override void ItemUse()
    {
        //Àåºñ½½·Ô¿¡ ÀåÂø
        Shared.GameMgr.PLAYER.GetEquipmentWindow().EquippedItem(this);
        Shared.GameMgr.PLAYER.GetInventory().DeleteItem(this);
    }

    public void SetRandomStat()
    {
        DicEquipmentItemStat[STAT_TYPE.ATK] = Random.Range(5f, 10f);
        DicEquipmentItemStat[STAT_TYPE.DEF] = Random.Range(2f, 5f);
    }

    public abstract bool Enhance();
    public abstract string GetEnhanceInfo();
    public abstract float GetItemStat();
    public abstract string GetStrSTAT_Type();

    public void SetEnhanceValue(int value) { EnhanceValue = value; }
    public int GetEnhanceValue() { return EnhanceValue; }
    public float GetEnhanceRisingAmount() { return EnhanceRisingAmount; }
    protected bool TryEnhance()
    {
        return Random.value < EnhanceProbability;
    }

    public float GetEnhanceProbability() { return EnhanceProbability; }

    public string GetEnhanceProbabilityColor()
    {
        string colorHex = "";

        if (EnhanceProbability >= 0.8f)
            colorHex = "#32CD32"; // LimeGreen
        else if (EnhanceProbability >= 0.5f)
            colorHex = "#FFD700"; // Gold
        else if (EnhanceProbability >= 0.2f)
            colorHex = "#FF8C00"; // DarkOrange
        else
            colorHex = "#FF4500"; // OrangeRed

       string color = $"<color=#FFFFFF>¼º°øÈ®·ü </color> : <color={colorHex}><b> {EnhanceProbability.ToString("P0")}</b></color>";
        return color;
    }

    public int GetEnhaceMaterial() { return EnhanceMaterial; }
}
