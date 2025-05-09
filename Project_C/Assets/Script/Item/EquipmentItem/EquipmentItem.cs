using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
public abstract class EquipmentItem : ItemBase
{
    public EQUITMENT_TYPE EquimentType;
    public SerializedDictionary<STAT_TYPE, float> DicEquipmentItemStat;

    protected int EnhanceValue;
    protected float EnhanceRisingAmount;
    protected float EnhanceProbability = 1.0f;
    public override void ItemUse()
    {
        //¿Â∫ÒΩΩ∑‘ø° ¿Â¬¯
        Shared.GameMgr.PLAYER.GetEquipmentWindow().EquippedItem(this);
        Shared.GameMgr.PLAYER.GetInventory().DeleteItem(this);
    }

    public void SetRandomStat()
    {
        DicEquipmentItemStat[STAT_TYPE.ATK] = Random.Range(5f, 10f);
        DicEquipmentItemStat[STAT_TYPE.DEF] = Random.Range(2f, 5f);
    }

    public abstract bool Enhance();
    public abstract string EnhanceInfo();

    public void SetEnhanceValue(int value) { EnhanceValue = value; }
    public int GetEnhanceValue() { return EnhanceValue; }
    protected bool TryEnhance()
    {
        return Random.value < EnhanceProbability;
    }
}
