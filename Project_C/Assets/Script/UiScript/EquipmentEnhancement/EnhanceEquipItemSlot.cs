using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceEquipItemSlot : ItemSlot
{
    public Text EnhanceValue;
    public Text ItemName;
    EquipmentItem EquipmentItem;
    public void Init(EquipmentItem _equipmentItem)
    {
        EquipmentItem = _equipmentItem;
        InputItem(EquipmentItem);
        EnhanceValue.text = "+" + EquipmentItem.GetEnhanceValue().ToString();
        ItemName.text = EquipmentItem.ItemName;
    }
    public override void OnClickSlot()
    {
        Debug.Log(EquipmentItem.ItemName);
    }
}
