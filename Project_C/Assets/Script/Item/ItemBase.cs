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
                return "��� ������";
            case ITEM_TYPE.EQUIPMENT:
                return "��� ������";
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
            case ITEM_GRADE.COMMON: //���
                return "FFFFFF";
            case ITEM_GRADE.UNCOMMON: //���
                return "7CFC00";
            case ITEM_GRADE.RARE: //�Ķ���
                return "1E90FF";
            case ITEM_GRADE.EPIC: //�����
                return "EE82EE";
            case ITEM_GRADE.LEGENDARY: //��Ȳ��
                return "FF8C00";
            default:
                return "FFFFFF";
        }
    }
}
