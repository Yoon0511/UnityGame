using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

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
        if(ItemBaseInfo == null)
            return null;

        switch((ITEM_TYPE)ItemBaseInfo.Type)
        {
            case ITEM_TYPE.EQUIPMENT:
                TableItem.EquipItemStatInfo EquipItemStatInfo = TableItem.GetEquipItemStatInfo(_id);
                TableItem.EquipMentItemInfo EquipMentItemInfo = TableItem.GetEquipMentItemInfo(_id);
                EquipmentItem Equipmentitem = Shared.PoolMgr.GetObject(ItemBaseInfo.Prefabs).GetComponent<EquipmentItem>();
                //EquipmentItem Equipmentitem = new Weapon();
                Equipmentitem.InputItemBaseData(ItemBaseInfo);
                Equipmentitem.InputEquipMentData(EquipMentItemInfo, EquipItemStatInfo);
                return Equipmentitem;

            case ITEM_TYPE.USE:
                TableItem.UseItemInfo UseItemInfo = TableItem.GetUseItemInfo(_id);
                Potion UseItem = Shared.PoolMgr.GetObject(ItemBaseInfo.Prefabs).GetComponent<Potion>();
                UseItem.InputItemBaseData(ItemBaseInfo);
                UseItem.InputUseItemData(UseItemInfo);
                return UseItem;

            default:
                return null;
        }       
    }

    public QuestBase GetQuest(int _id)
    {
        TableQuest.QuestBaseInfo QuestBaseInfo = TableQuest.GetQuestBaseInfo(_id);
        if(QuestBaseInfo == null)
            return null;

        switch ((QUEST_TYPE)QuestBaseInfo.Type)
        {
            case QUEST_TYPE.HUNTING:
                TableQuest.HuntingQuestInfo HuntingQuestInfo = TableQuest.GetHuntingQuestInfo(_id);
                HuntingQuest huntingQuset = new HuntingQuest();
                huntingQuset.InputQuestBaseData(QuestBaseInfo);
                huntingQuset.InputHuntingQuestData(HuntingQuestInfo);
                return huntingQuset;

            case QUEST_TYPE.MEETING:
                TableQuest.NPCMeetingQuestInfo NPCMeetingQuestInfo = TableQuest.GetNPCMeetingQuestInfo(_id);
                NpcMeetingQuest NpcMeetingQuest = new NpcMeetingQuest();
                NpcMeetingQuest.InputQuestBaseData(QuestBaseInfo);
                NpcMeetingQuest.InputNPCMeetingData(NPCMeetingQuestInfo);
                return NpcMeetingQuest;

            default:
                return null;
        }
    }
}
