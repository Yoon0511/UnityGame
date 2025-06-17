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
    public int SellPrice;
    public int BuyPrice;
    public ITEM_GRADE Grade;

    public abstract void ItemUse();
    public abstract string GetItemExplanation();

    public void InputItemBaseData(TableItem.ItemBaseInfo _info)
    {
        ItemType = (ITEM_TYPE)_info.Type;
        ItemName = _info.ItemName;
        Id = _info.Id;
        SpriteName = _info.SpriteName;
        SellPrice = _info.SellPrice;
        BuyPrice = _info.BuyPrice;
        Grade = (ITEM_GRADE)_info.Grade;
    }

    public string GetItemTypeText()
    {
        switch(ItemType)
        {
            case ITEM_TYPE.USE:
                return "사용 아이템";
            case ITEM_TYPE.EQUIPMENT:
                return "장비 아이템";
            default:
                return null;
        }
    }

    public virtual string GetItemName()
    {
        return $"<color=#{GetStrGradeColor()}><b>{ItemName}</b></color>";
    }

    protected string GetStrGradeColor()
    {
        switch(Grade)
        {
            case ITEM_GRADE.COMMON: //흰색
                return "FFFFFF";
            case ITEM_GRADE.UNCOMMON: //녹색
                return "7CFC00";
            case ITEM_GRADE.RARE: //파란색
                return "1E90FF";
            case ITEM_GRADE.EPIC: //보라색
                return "EE82EE";
            case ITEM_GRADE.LEGENDARY: //주황색
                return "FF8C00";
            default:
                return "FFFFFF";
        }
    }
}
