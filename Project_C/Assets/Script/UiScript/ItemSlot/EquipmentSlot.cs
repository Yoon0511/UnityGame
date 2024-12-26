using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : ItemSlot
{
    [SerializeField]
    EquipmentWindow equipmentWindow;

    EquipmentItem quipmentItem;
    public override void OnClickSlot() //��� ����
    {
        Shared.GameMgr.PLAYER.GetComponent<Player>().GetInventory().AddItem(quipmentItem);
        equipmentWindow.Unequip(quipmentItem.equimentType);
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
