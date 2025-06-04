using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreItemSlot : ItemSlot
{
    [SerializeField]
    Text ITEMPRICE;
    [SerializeField]
    Text ITEMNAME;

    [NonSerialized]
    public int Price;
    [NonSerialized]
    public string ItemName;

    [NonSerialized]
    public Store Store;
    public override void InputItem(ItemBase _Item)
    {
        base.InputItem(_Item);

        ITEMPRICE.text = Item.BuyPrice.ToString();
        ITEMNAME.text = Item.ItemName;
    }

    public override void OnClickSlot()
    {
        Store.OnBuyPopup(Item);
    }
}
