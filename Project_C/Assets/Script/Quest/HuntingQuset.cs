using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

//
public class HuntingQuset : QuestBase
{
    public int CurrentGoalCount;
    public int GoalCount;
    public int TargetId;
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
                Complete();
            }
        }
    }

    public override void Accept()
    {
     
    }

    public override void Refusal()
    {
        
    }

    public override void Complete() //����
    {
        UnityEngine.Debug.Log("HuntingQuest Complete");
        IsComplete = true;
    }

    public override void Fail()
    {

    }

    public override string GetContents()
    {
        string TextGoal = "<color=#CCCCCC><b>" + GoalCount.ToString() + "</b></color>";
        string TextCrrentGoal = "<color=#FF6B6B><b>" + CurrentGoalCount.ToString() + "</b></color>";
        string contents = Contents + " " + TextCrrentGoal + "/" + TextGoal;
        return contents;
    }

    public override string GetRewardDetail()
    {
        string TextExp = "<color=#ADFF2F><b>" + "EXP" + "</b></color>";
        string TextReward = "<color=#00FF00>" + GetReward().ToString() + "</color>";
        string detail = TextExp + " - "+ TextReward;
        return detail;
    }
}
