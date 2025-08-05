using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
    public SaveData SaveData()
    {
        SaveData SaveData = new SaveData();

        SaveData.TransformData.Init(transform);
        SaveData.StatData = GetStatData().ToJsonData();
        SaveData.InventoryData = GetInventory().ToJsonData();
        SaveData.EquipmentItemData = EquipmentItemToJson();
        SaveData.CurrSkillData = CurrSkillToJson();
        SaveData.CompletedQuestData = GetCompletedQuest();

        return SaveData;
    }

    public void Load(SaveData _saveData)
    {
        ApplyTransformData(_saveData.TransformData);
        GetStatData().ApplyJsonData(_saveData.StatData);
        GetInventory().ApplyJsonData(_saveData.InventoryData);
        ApplyEquipmentItemData(_saveData.EquipmentItemData);
        ApplyCurrSkillData(_saveData.CurrSkillData);

        GetInventory().Refresh();
    }

    void ApplyTransformData(TransformDataJson _transformData)
    {
        transform.position = _transformData.Position;
        transform.eulerAngles = _transformData.EulerAngles;
        transform.localScale = _transformData.Scale;
    }
}
