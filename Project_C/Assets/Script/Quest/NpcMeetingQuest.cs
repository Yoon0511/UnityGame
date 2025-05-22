using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMeetingQuest : QuestBase
{
    [SerializeField]
    int TargetNPCId;
    [SerializeField]
    string TargetNPCName;

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
        
    }

    public override void Complete()
    {
        GiveQuestReward();

        IsComplete = true;

        //완료 안내 메시지
        Shared.UiMgr.CreateSystemMsg(GetQusetName() + "완료!", SYSTEM_MSG_TYPE.QUEST_COMPLETE);
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
            Complete();
        }
    }

    public override void Refusal()
    {
        
    }
}
