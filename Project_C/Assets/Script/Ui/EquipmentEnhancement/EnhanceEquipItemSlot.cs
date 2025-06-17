using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceEquipItemSlot : ItemSlot
{
    public Text ItemName;
    EquipmentItem EquipmentItem;
    EnhanceView EnhanceView;
    public void Init(EquipmentItem _equipmentItem,EnhanceView _enhanceView)
    {
        EquipmentItem = _equipmentItem;
        InputItem(EquipmentItem);

        ItemName.text = EquipmentItem.GetItemName();

        EnhanceView = _enhanceView;
    }
    public override void OnClickSlot()
    {
        EnhanceView.Init(EquipmentItem);
    }

    public override void ReleaseObject()
    {
        Pool.Release(gameObject);
    }
}
