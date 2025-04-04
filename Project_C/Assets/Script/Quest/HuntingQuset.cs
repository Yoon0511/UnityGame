using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

//
public class HuntingQuset : QuestBase
{
    public int CurrentGoalCount;
    public int GoalCount;
    public int TargetId;
    public void Init(string _name,string _contents,int _goalcount,int _targetid,int _reward)
    {
        GoalCount = _goalcount;
        TargetId = _targetid;
        SetQusetName(_name);
        SetContents(_contents);
        SetReward(_reward);
        QuestType = (int)QUEST_TYPE.HUNTING;
    }
    public override void Progress(QuestMsgBase _questmsg)
    {
        if (_questmsg == null && _questmsg.GetQuestType() != QuestType)
            return;

        if(TargetId == _questmsg.GetDicMsgValue("TargetId"))
        {
            CurrentGoalCount++;
            UnityEngine.Debug.Log(CurrentGoalCount + " - " + GoalCount);
            if(CurrentGoalCount == GoalCount) //목표치 달성
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

    public override void Complete()
    {
        UnityEngine.Debug.Log("HuntingQuest Complete");
        IsComplete = true;
    }

    public override void Fail()
    {

    }
}
