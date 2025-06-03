using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : NPC
{
    QuestBase Quest;
    public int QuestId;

    public override void Init()
    {
        base.Init();
        Quest = Shared.DataMgr.GetQuest(QuestId);
        Quest.SetOwnerNPC(this);
        //int count = Quest.GetConversation().Length;
        //ConverstationTexts = new string[count];
        //Array.Copy(Quest.GetConversation(), ConverstationTexts, count);
    }
    public override void RayTargetEvent(Character _character)
    {
        //base.RayTargetEvent(this);
        SendQuestMsg();
        Shared.GameMgr.OnNPCDialogueWindow(this, (Player)_character);
    }

    public override string[] GetConverstationTexts()
    {
        if(Quest.GetQuestState() != QUEST_STATE.END)
        {
            return Quest.GetConversation();
        }
        return base.GetConverstationTexts();
    }
    public void QuestAccpect(Player _player)
    {
        _player.AddQuest(Quest);

        Quest.Accept();
        Quest.SetProgressPlayer(_player);
    }

    public void QuestRefusal()
    {
        Quest.Refusal();
    }

    public void QuestComplete()
    {
        Quest.Complete();
    }

    public QuestBase GetQuest() {  return Quest; }

    public override void UpdateMiniMapIcon()
    {
        Quest.UpdateMiniMapIcon();
    }
}
