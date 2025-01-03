using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField]
    EquipmentWindow equiment;
    public EquipmentWindow GetEquipmentWindow() { return equiment; }
    public void ApplyEquipItem(EquipmentItem _equipmentItem, bool UnEquip = false)
    {
        for (int i = 1; i < (int)STAT_TYPE.ENUM_END; ++i)
        {
            float statValue = 0.0f;
            bool IsInStat = _equipmentItem.dic_EquipmentItemStat.TryGetValue((STAT_TYPE)i, out statValue);

            if (IsInStat && UnEquip == false) //��� ���� �߰�
            {
                statdata.EnhanceStat((STAT_TYPE)i, statValue);
            }
            else if (IsInStat && UnEquip) //��� ���� ����
            {
                statdata.EnhanceStat((STAT_TYPE)i, -statValue);
            }
        }
    }
}
