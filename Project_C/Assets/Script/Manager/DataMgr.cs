using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : MonoBehaviour
{
    private TableItem TableItem = new TableItem();
    private TableQuest TableQuest = new TableQuest();
    private void Awake()
    {
        Shared.DataMgr = this;
        Init();
    }

    public void Init()
    {
        TableItem.Init_CSV(1, 0);
        TableQuest.Init_CSV(1, 0);
    }
    public ItemBase GetItem(int _id)
    {
        TableItem.ItemBaseInfo ItemBaseInfo = TableItem.GetItemBaseInfo(_id);
        TableItem.EquipItemStatInfo EquipItemStatInfo = TableItem.GetEquipItemStatInfo(_id);
        TableItem.EquipMentItemInfo EquipMentItemInfo = TableItem.GetEquipMentItemInfo(_id);
        TableItem.UseItemInfo UseItemInfo = TableItem.GetUseItemInfo(_id);

        if (ItemBaseInfo.Type == (int)ITEM_TYPE.EQUIPMENT)
        {
            EquipmentItem Equipmentitem = Shared.PoolMgr.GetObject(ItemBaseInfo.Prefabs).GetComponent<EquipmentItem>();
            Equipmentitem.InputItemBaseData(ItemBaseInfo);
            Equipmentitem.InputEquipMentData(EquipMentItemInfo, EquipItemStatInfo);

            return Equipmentitem;
        }
        else if(ItemBaseInfo.Type == (int) ITEM_TYPE.USE)
        {
            HP_Postion UseItem = Shared.PoolMgr.GetObject(ItemBaseInfo.Prefabs).GetComponent<HP_Postion>();
            UseItem.InputItemBaseData(ItemBaseInfo);
            UseItem.InputUseItemData(UseItemInfo);

            return UseItem;
        }
        
        return null;
    }

    public QuestBase GetQuest(int _id)
    {
        TableQuest.QuestBaseInfo QuestBaseInfo = TableQuest.GetQuestBaseInfo(_id);
        TableQuest.HuntingQuestInfo HuntingQuestInfo = TableQuest.GetHuntingQuestInfo(_id);
        TableQuest.NPCMeetingQuestInfo NPCMeetingQuestInfo = TableQuest.GetNPCMeetingQuestInfo(_id);
        

        switch (QuestBaseInfo.Type)
        {
            case (int)QUEST_TYPE.HUNTING:
                HuntingQuset huntingQuset = new HuntingQuset();
                huntingQuset.InputQuestBaseData(QuestBaseInfo);
                huntingQuset.InputHuntingQuestData(HuntingQuestInfo);
                return huntingQuset;
            case (int)QUEST_TYPE.MEETING:
                NpcMeetingQuest NpcMeetingQuest = new NpcMeetingQuest();
                NpcMeetingQuest.InputQuestBaseData(QuestBaseInfo);
                NpcMeetingQuest.InputNPCMeetingData(NPCMeetingQuestInfo);
                return NpcMeetingQuest;
            default:
                return null;
        }
    }
}
