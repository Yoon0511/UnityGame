using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestNPC : NPC
{
    public QuestBase Quest;

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
