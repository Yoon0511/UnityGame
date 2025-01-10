using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;
public class EquipmentItem : Item
{
    public EQUITMENT_TYPE equimentType;
    public SerializedDictionary<STAT_TYPE, float> dic_EquipmentItemStat;

    public override void ItemUse()
    {
        //¿Â∫ÒΩΩ∑‘ø° ¿Â¬¯
        Shared.GameMgr.PLAYER.GetEquipmentWindow().EquippedItem(this);
        Shared.GameMgr.PLAYER.GetInventory().DeleteItem(this);
    }

    public void SetRandomStat()
    {
        dic_EquipmentItemStat[STAT_TYPE.ATK] = Random.Range(5f, 10f);
        dic_EquipmentItemStat[STAT_TYPE.DEF] = Random.Range(2f, 5f);
    }
}
