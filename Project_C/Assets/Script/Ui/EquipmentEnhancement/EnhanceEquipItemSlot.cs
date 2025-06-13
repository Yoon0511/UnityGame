using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceEquipItemSlot : ItemSlot
{
    public Text EnhanceValue;
    public Text ItemName;
    EquipmentItem EquipmentItem;
    EnhanceView EnhanceView;
    public void Init(EquipmentItem _equipmentItem,EnhanceView _enhanceView)
    {
        EquipmentItem = _equipmentItem;
        InputItem(EquipmentItem);

        EnhanceValue.text = "+" + EquipmentItem.GetEnhanceValue().ToString();
        ItemName.text = EquipmentItem.GetItemName();

        EnhanceView = _enhanceView;
    }
    public override void OnClickSlot()
    {
        EnhanceView.Init(EquipmentItem);
    }
}
