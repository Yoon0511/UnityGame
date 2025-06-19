using AYellowpaper.SerializedCollections;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField]
    EquipmentWindow equiment;

    public SerializedDictionary<EQUITMENT_TYPE, EquipmentItem> DicEquitmentItems = new SerializedDictionary<EQUITMENT_TYPE, EquipmentItem>();
    
    public SerializedDictionary<EQUITMENT_TYPE, EquipmentItem> GetDicEquitmentItem()
    { 
        return DicEquitmentItems;
    }

    public EquipmentWindow GetEquipmentWindow() { return equiment; }
    //장비 장착 및 해제
    public void ApplyEquipItem(EquipmentItem _equipmentItem, bool UnEquip = false)
    {
        Shared.SoundMgr.PlaySFX("ITEM_EQUIP");
        for (int i = 1; i < (int)STAT_TYPE.ENUM_END; ++i)
        {
            float statValue = 0.0f;
            bool IsInStat = _equipmentItem.DicEquipmentItemStat.TryGetValue((STAT_TYPE)i, out statValue);

            if (IsInStat && UnEquip == false) //장비 스탯 추가
            {
                Statdata.EnhanceStat((STAT_TYPE)i, statValue);
            }
            else if (IsInStat && UnEquip) //장비 스탯 해제
            {
                Statdata.EnhanceStat((STAT_TYPE)i, -statValue);
            }
        }
    }
}
