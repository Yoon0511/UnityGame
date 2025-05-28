using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : NPC
{
    public QuestBase Quest;

    public override void Init()
    {
        base.Init();
        Quest = Shared.DataMgr.GetQuest(2001);
        int count = Quest.GetConversation().Length;
        ConverstationTexts = new string[count];
        Array.Copy(Quest.GetConversation(), ConverstationTexts, count);
    }
    public override void RayTargetEvent(Character _character)
    {
        base.RayTargetEvent(this);
        Shared.GameMgr.OnNPCDialogueWindow(this, (Player)_character);
    }

    public void QuestAccpect(Player _player)
    {
        _player.AddQuest(Quest);

        Quest.SetProgressPlayer(_player);
    }

    public void QuestRefusal()
    {
        Quest.Refusal();
    }
}
