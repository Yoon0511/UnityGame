using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBase : PoolAble
{
    public ITEM_TYPE ItemType;
    public string ItemName;
    public int Id;
    public string SpriteName;
    public Character Owner;

    public abstract void ItemUse();

    public void InputItemBaseData(TableItem.ItemBaseInfo _info)
    {
        ItemType = (ITEM_TYPE)_info.Type;
        ItemName = _info.ItemName;
        Id = _info.Id;
        SpriteName = _info.SpriteName;
    }
}
