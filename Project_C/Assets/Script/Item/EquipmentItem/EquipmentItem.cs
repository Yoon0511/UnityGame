using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
public abstract class EquipmentItem : ItemBase
{
    public EQUITMENT_TYPE EquimentType;
    public SerializedDictionary<STAT_TYPE, float> DicEquipmentItemStat;

    public int EnhanceValue;
    public float EnhanceRisingAmount;
    protected float EnhanceProbability = 1.0f;
    protected float EnhanceProbabilityDecreaseAmount;
    protected int EnhanceMaterial;
    protected int MaterialRisingAmount;

    public EquipmentItem()
    {
        EnhanceMaterial = 100;
        MaterialRisingAmount = 30;
    }
    public override void ItemUse()
    {
        //��񽽷Կ� ����
        Shared.GameMgr.PLAYER.GetEquipmentWindow().EquippedItem(this);
        Shared.GameMgr.PLAYER.GetInventory().DeleteItem(this);
    }
    //��� ������ ȣ��
    public virtual void UnEquip()
    {

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

       string color = $"<color=#FFFFFF>����Ȯ�� </color> : <color={colorHex}><b> {EnhanceProbability.ToString("P0")}</b></color>";
        return color;
    }

    public int GetEnhaceMaterial() { return EnhanceMaterial; }
    public override string GetItemExplanation()
    {
        return null;
    }

    public void InputEquipMentData(TableItem.EquipMentItemInfo _info, TableItem.EquipItemStatInfo _stat)
    {
        //EquipMentItemInfo
        EquimentType            = (EQUITMENT_TYPE)_info.EquipmentType;
        EnhanceValue            = _info.EnhanceValue;
        EnhanceRisingAmount     = _info.EnhanceRisingAmount;
        EnhanceProbability      = _info.EnhanceProbability;
        EnhanceProbabilityDecreaseAmount      = _info.EnhanceProbabilityDecreaseAmount;
        EnhanceMaterial         = _info.EnhanceMaterial;
        MaterialRisingAmount    = _info.MaterialRisingAmount;

        //Stat
        DicEquipmentItemStat[(STAT_TYPE)_stat.StatType] = _stat.Value;
    }

    public override string GetItemName()
    {
        return $"<color=#{GetStrGradeColor()}><b>+{EnhanceValue} {ItemName}</b></color>";
    }
}
