using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class TableItem : TableBase
{
    [Serializable]
    public class ItemBaseInfo
    {
        public int Id;
        public byte Type;
        public string Prefabs;
        public string ItemName;
        public string SpriteName;
        public int SellPrice;
        public int BuyPrice;
        public int Grade;
    }

    [Serializable]
    public class EquipMentItemInfo
    {
        public int Id;
        public byte EquipmentType;
        public int EnhanceValue;
        public float EnhanceRisingAmount;
        public float EnhanceProbability;
        public float EnhanceProbabilityDecreaseAmount;
        public int EnhanceMaterial;
        public int MaterialRisingAmount;
    }

    [Serializable]
    public class EquipItemStatInfo
    {
        public int Id;
        public int StatType;
        public float Value;
    }

    [Serializable]
    public class UseItemInfo
    {
        public int Id;
        public int PotionType;
        public float Amount;
    }

    string[] ItemFile = { "Item(ItemBase)", "Item(EquipMentItem)", "Item(EquipMentItemStat)", "Item(UseItem)" };
    enum File
    {
        ItemBase,
        EquipMentItem,
        EquipMentItemStat,
        UseItem,
    }

    public Dictionary<int, ItemBaseInfo> DicItemBase = new Dictionary<int, ItemBaseInfo>();
    public Dictionary<int, EquipMentItemInfo> DicEquipMentItem = new Dictionary<int, EquipMentItemInfo>();
    public Dictionary<int, EquipItemStatInfo> DicEquipMentStat = new Dictionary<int, EquipItemStatInfo>();
    public Dictionary<int, UseItemInfo> DicUseItem = new Dictionary<int, UseItemInfo>();

    public void Init_CSV(int _Row, int _Col)
    {
        // ItemBase
        CSVReader reader = GetCSVReader(ItemFile[(int)File.ItemBase]);

        for (int row = _Row; row < reader.row; ++row)
        {
            ItemBaseInfo info = new ItemBaseInfo();

            if (Read(reader, info, row, _Col) == false)
                break;

            DicItemBase.Add(info.Id, info);
        }

        // EquipMentItem
        reader = GetCSVReader(ItemFile[(int)File.EquipMentItem]);

        for (int row = _Row; row < reader.row; ++row)
        {
            EquipMentItemInfo info = new EquipMentItemInfo();

            if (Read(reader, info, row, _Col) == false)
                break;

            DicEquipMentItem.Add(info.Id, info);
        }

        // EquipMentItemStat
        reader = GetCSVReader(ItemFile[(int)File.EquipMentItemStat]);

        for (int row = _Row; row < reader.row; ++row)
        {
            EquipItemStatInfo info = new EquipItemStatInfo();

            if (Read(reader, info, row, _Col) == false)
                break;

            DicEquipMentStat.Add(info.Id, info);
        }

        // UseItem
        reader = GetCSVReader(ItemFile[(int)File.UseItem]);

        for (int row = _Row; row < reader.row; ++row)
        {
            UseItemInfo info = new UseItemInfo();

            if (Read(reader, info, row, _Col) == false)
                break;

            DicUseItem.Add(info.Id, info);
        }
    }

    // ItemBase
    protected bool Read(CSVReader _Reader, ItemBaseInfo _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.get(_Row, ref _Info.Id);
        _Reader.get(_Row, ref _Info.Type);
        _Reader.get(_Row, ref _Info.Prefabs);
        _Reader.get(_Row, ref _Info.ItemName);
        _Reader.get(_Row, ref _Info.SpriteName);
        _Reader.get(_Row, ref _Info.SellPrice);
        _Reader.get(_Row, ref _Info.BuyPrice);
        _Reader.get(_Row, ref _Info.Grade);

        return true;
    }

    // EquipMentItem
    protected bool Read(CSVReader _Reader, EquipMentItemInfo _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.get(_Row, ref _Info.Id);
        _Reader.get(_Row, ref _Info.EquipmentType);
        _Reader.get(_Row, ref _Info.EnhanceValue);
        _Reader.get(_Row, ref _Info.EnhanceRisingAmount);
        _Reader.get(_Row, ref _Info.EnhanceProbability);
        _Reader.get(_Row, ref _Info.EnhanceProbabilityDecreaseAmount);
        _Reader.get(_Row, ref _Info.EnhanceMaterial);
        _Reader.get(_Row, ref _Info.MaterialRisingAmount);

        return true;
    }

    // EquipMentItemStat
    protected bool Read(CSVReader _Reader, EquipItemStatInfo _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.get(_Row, ref _Info.Id);
        _Reader.get(_Row, ref _Info.StatType);
        _Reader.get(_Row, ref _Info.Value);

        return true;
    }

    // UseItem
    protected bool Read(CSVReader _Reader, UseItemInfo _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.get(_Row, ref _Info.Id);
        _Reader.get(_Row, ref _Info.PotionType);
        _Reader.get(_Row, ref _Info.Amount);

        return true;
    }

    public void PrintItemBase()
    {
        foreach(var kvp in DicItemBase)
        {
            Debug.Log($"Key: {kvp.Key} | Value: {kvp.Value.Type}");
            Debug.Log($"Key: {kvp.Key} | Value: {kvp.Value.Prefabs}");
            Debug.Log($"Key: {kvp.Key} | Value: {kvp.Value.ItemName}");
            Debug.Log($"Key: {kvp.Key} | Value: {kvp.Value.SpriteName}");
            Debug.Log($"Key: {kvp.Key} | Value: {kvp.Value.SellPrice}");
            Debug.Log($"Key: {kvp.Key} | Value: {kvp.Value.BuyPrice}");
        }
    }

    public void PrintEquipMentItem()
    {
        foreach (var kvp in DicEquipMentItem)
        {
            var item = kvp.Value;
            Debug.Log(
                $"Key: {kvp.Key} | EnhanceValue: {item.EnhanceValue} | EnhanceRisingAmount: {item.EnhanceRisingAmount} | " +
                $"EnhanceProbability: {item.EnhanceProbability} | EnhanceMaterial: {item.EnhanceMaterial} | MaterialRisingAmount: {item.MaterialRisingAmount}"
            );
        }
    }

    public void PrintEquipItemStat()
    {
        foreach (var kvp in DicEquipMentStat)
        {
            var item = kvp.Value;
            Debug.Log(
                $"Key: {kvp.Key} | StatType: {item.StatType} | Value: {item.Value} | ");
        }
    }

    public void PrintUseItem()
    {
        foreach (var kvp in DicUseItem)
        {
            var item = kvp.Value;
            Debug.Log(
                $"Key: {kvp.Key} | Amount: {item.Amount}");
        }
    }

    public ItemBaseInfo GetItemBaseInfo(int _id)
    {
        if (DicItemBase.TryGetValue(_id, out var info))
            return info;

        Debug.LogWarning($"[DataMgr] ItemBaseInfo ID {_id}가 존재하지 않습니다.");
        return null;
    }

    public EquipMentItemInfo GetEquipMentItemInfo(int _id)
    {
        if (DicEquipMentItem.TryGetValue(_id, out var info))
            return info;

        Debug.LogWarning($"[DataMgr] EquipMentItemInfo ID {_id}가 존재하지 않습니다.");
        return null;
    }

    public EquipItemStatInfo GetEquipItemStatInfo(int _id)
    {
        if (DicEquipMentStat.TryGetValue(_id, out var info))
            return info;

        Debug.LogWarning($"[DataMgr] EquipItemStatInfo ID {_id}가 존재하지 않습니다.");
        return null;
    }

    public UseItemInfo GetUseItemInfo(int _id)
    {
        if (DicUseItem.TryGetValue(_id, out var info))
            return info;

        Debug.LogWarning($"[DataMgr] UseItemInfo ID {_id}가 존재하지 않습니다.");
        return null;
    }
}
