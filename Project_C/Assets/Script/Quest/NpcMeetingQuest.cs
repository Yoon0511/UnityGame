using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMeetingQuest : QuestBase
{
    [SerializeField]
    int TargetNPCId;
    [SerializeField]
    string TargetNPCName;

    bool IsAutoComplete;

    private void Start()
    {
        SetQusetName(GetContents());
    }
    public void Init(NPC _owner,int _questId,string _questName,int _targetNpcId,string _targetNpcName,int _reward)
    {
        OwnerNPC = _owner;
        Id = _questId;
        SetQusetName(GetContents());
        SetReward(_reward);
        TargetNPCId = _targetNpcId;
        TargetNPCName = _targetNpcName;
        QuestType = (int)QUEST_TYPE.MEETING;
    }
    public override void Accept()
    {
        StateChange(QUEST_STATE.PROGRESS);
        UpdateMiniMapIcon();
    }

    public override void Complete()
    {
        IsComplete = true;
        //StateChange(QUEST_STATE.COMPLETE);
        GiveQuestReward();
        UpdateMiniMapIcon();
        StateChange(QUEST_STATE.END);
    }

    public override void Fail()
    {
        
    }

    public override string GetContents()
    {
        string TargetName = "<color=#0EA5E9><b>" + TargetNPCName + "</b></color>";
        string TextMeet = " 만나기";
        Contents = TargetName + TextMeet;
        return Contents;
    }

    public override string GetRewardDetail()
    {
        string TextReward = "<color=#00FF00>" + GetReward().ToString() + "</color>";
        string detail = GetRewardTypeText() + " - " + TextReward;
        return detail;
    }

    public override void Progress(QuestMsgBase _questmsg)
    {
        if (_questmsg == null && _questmsg.GetQuestType() != QuestType)
            return;

        if(TargetNPCId == _questmsg.GetDicMsgValue("TargetId"))
        {
            if(IsAutoComplete)
            {
                StateChange(QUEST_STATE.COMPLETE);
                Complete();
            }
            else
            {
                StateChange(QUEST_STATE.COMPLETE);
            }
        }
    }

    public override void Refusal()
    {
        
    }

    public void InputNPCMeetingData(TableQuest.NPCMeetingQuestInfo _info)
    {
        TargetNPCId = _info.TargetNPCId;
        TargetNPCName = _info.TargetNPCName;
        IsAutoComplete = Convert.ToBoolean(_info.IsAutoComplete);
    }

    public override void UpdateMiniMapIcon()
    {
        if (OwnerNPC.HasMapIcon() == false)
            return;

        switch (QuestState)
        {
            case QUEST_STATE.START: // 시작가능상태 - 미니맵 아이콘 = (!)
                {
                    OwnerNPC.AllUpdateMapIcon("Exclamation_mark", 12, 12);
                    break;
                }
            case QUEST_STATE.PROGRESS: // 진행중 - 타겟NPC아이콘 변경 - 미니맵 아이콘 = (?)
                {
                    OwnerNPC.AllUpdateMapIcon("NPC", 5, 5);
                    NPC npc = Shared.GameMgr.GetNPCinList(TargetNPCId);
                    npc.AllUpdateMapIcon("Question_mark", 12, 12);

                    QuestNPC questNPC = Shared.GameMgr.GetNPCinList(TargetNPCId) as QuestNPC;
                    if(questNPC != null)
                    {
                        questNPC.SetQuest(this);
                    }
                    break;
                }
            case QUEST_STATE.COMPLETE:
                Shared.GameMgr.GetNPCinList(TargetNPCId).UpdateMapIcon();
                //Shared.GameMgr.GetNPCinList(TargetNPCId).AllUpdateMapIcon("NPC", 5, 5);
                //Shared.GameMgr.GetNPCinList(TargetNPCId).UpdateMiniMapIcon();
                break;
            default: // 기본 아이콘
                OwnerNPC.AllUpdateMapIcon("Exclamation_mark", 12, 12);
                break;
        }
    }
}
