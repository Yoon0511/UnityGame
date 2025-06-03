using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor.Timeline;

//
public class HuntingQuest : QuestBase
{
    public int CurrentGoalCount;
    public int GoalCount;
    [NonSerialized]
    public int TargetId;
    public MONSTER_ID MONSTER_ID;
    
    private void Start()
    {
        TargetId = (int)MONSTER_ID;
    }
    public void Init(int _id,string _name,string _contents,int _goalcount,int _targetid,int _reward,NPC _owner)
    {
        Id = _id;
        GoalCount = _goalcount;
        TargetId = _targetid;
        SetQusetName(_name);
        SetContents(_contents);
        SetReward(_reward);
        QuestType = (int)QUEST_TYPE.HUNTING;
        OwnerNPC = _owner;
        QuestState = QUEST_STATE.START;
    }
    public override void Progress(QuestMsgBase _questmsg)
    {
        if (_questmsg == null && _questmsg.GetQuestType() != QuestType)
            return;

        if(TargetId == _questmsg.GetDicMsgValue("TargetId"))
        {
            CurrentGoalCount++;
            //UnityEngine.Debug.Log(CurrentGoalCount + " - " + GoalCount);
            if(CurrentGoalCount == GoalCount) //��ǥġ �޼�
            {
                //Complete();
                StateChange(QUEST_STATE.COMPLETE);
                UpdateMiniMapIcon();
            }
        }
    }

    public override void Accept()
    {
        StateChange(QUEST_STATE.PROGRESS);
        UpdateMiniMapIcon();
    }

    public override void Refusal()
    {
        
    }

    public override void Complete() //����
    {
        IsComplete = true;
        StateChange(QUEST_STATE.END);
        UpdateMiniMapIcon();
        GiveQuestReward();
    }

    public override void Fail()
    {

    }

    public override string GetContents()
    {
        string Goalcolor = "CCCCCC";
        string CrrentGoalcolor = "FF6B6B";
        string ContentsColor = "FFFFFF";

        if (QuestState == QUEST_STATE.COMPLETE ||
            QuestState == QUEST_STATE.END)
        {
            Goalcolor = "808080";
            CrrentGoalcolor = "808080";
            ContentsColor = "808080";
        }

        string TextGoal = $"<color=#{Goalcolor}><b>" + GoalCount.ToString() + "</b></color>";
        string TextCrrentGoal = $"<color=#{CrrentGoalcolor}><b>" + CurrentGoalCount.ToString() + "</b></color>";
        string contents = $"<color=#{ContentsColor}><b>{Contents}</b></color>" + " " + TextCrrentGoal + $"<color=#{ContentsColor}>/</color>" + TextGoal;


        if (QuestState == QUEST_STATE.COMPLETE)
        {
            contents += "\n <color=#00BFFF><b>�Ϸᰡ��!</b></color>";
        }

        return contents;
    }

    public override string GetRewardDetail()
    {
        string TextReward = "<color=#00FF00>" + GetReward().ToString() + "</color>";
        string detail = GetRewardTypeText() + " - "+ TextReward;
        return detail;
    }

    public void InputHuntingQuestData(TableQuest.HuntingQuestInfo _info)
    {
        CurrentGoalCount = _info.CurrentGoalCount;
        GoalCount = _info.GoalCount;
        TargetId =  _info.TargetId;
    }

    public override void UpdateMiniMapIcon()
    {
        if (OwnerNPC.HasMapIcon() == false)
            return;

        switch(QuestState)
        {
            case QUEST_STATE.START: // ���۰��ɻ��� - �̴ϸ� ������ = (!)
                OwnerNPC.AllUpdateMapIcon("Exclamation_mark", 12, 12);
                //OwnerNPC.GetMiniMapIcon().SetImage("Exclamation_mark");
                //OwnerNPC.GetMiniMapIcon().SetIconSize(12, 12);
                break;
            case QUEST_STATE.COMPLETE: // �Ϸᰡ�� - �̴ϸ� ������ = (?)
                OwnerNPC.AllUpdateMapIcon("Question_mark", 12, 12);
                //OwnerNPC.GetMiniMapIcon().SetImage("Question_mark");
                //OwnerNPC.GetMiniMapIcon().SetIconSize(12, 12);
                break;
            default: // �⺻ ������
                OwnerNPC.AllUpdateMapIcon("NPC", 5, 5);
                //OwnerNPC.GetMiniMapIcon().SetImage("NPC");
                //OwnerNPC.GetMiniMapIcon().SetIconSize(5, 5);
                break;
        }
    }
}
