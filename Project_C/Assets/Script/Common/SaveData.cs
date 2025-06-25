using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public class SaveData
{
    public TransformDataJson TransformData;
    public StatDataJson StatData;
    public InventoryJson InventoryData;
    public EquipmentItemJson EquipmentItemData;
    public CurrSkillJson CurrSkillData;

    public SaveData()
    {
        TransformData = new TransformDataJson();
    }
}
[Serializable]
public class TransformDataJson
{
    public Vector3 Position;
    public Vector3 EulerAngles;
    public Vector3 Scale;

    public void Init(Transform _transform)
    {
        Position = _transform.position;
        EulerAngles = _transform.eulerAngles;
        Scale = _transform.localScale;
    }
}
[Serializable]
public class StatDataJson
{
    public List<StatEntry> ListStat = new List<StatEntry>();

    [Serializable]
    public class StatEntry
    {
        public STAT_TYPE type;
        public float value;
    }

    public void Print()
    {
        Debug.Log("===== Loaded Stat Data =====");
        foreach (var entry in ListStat)
        {
            Debug.Log($"{entry.type}: {entry.value}");
        }
    }
}

[Serializable]
public class InventoryJson
{
    public List<int> ListItemId = new List<int>();
    public int Gold;
}

[Serializable]
public class EquipmentItemJson
{
    public List<int> ListItemId = new List<int>();
}

[Serializable]
public class CurrSkillJson
{
    public List<int> ListCurrSkillId = new List<int>();
}
