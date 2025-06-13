using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    [SerializeField]
    EquipmentWindow equipmentWindow;

    EquipmentItem quipmentItem;
    public override void OnClickSlot() //장비 해제
    {
        Shared.GameMgr.PLAYER.GetInventory().AddItem(quipmentItem);
        equipmentWindow.Unequip(quipmentItem.EquimentType);
        UnEquipmentItem();
    }

    public void InputEquipmentItem(EquipmentItem _item)
    {
        quipmentItem = _item;
        InputItem(_item);
    }

    public void UnEquipmentItem()
    {
        DeleteItem();
        quipmentItem = null;
    }

    public EquipmentItem GetEquitmentItem() { return quipmentItem; }
}
