using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

//
public class HuntingQuset : QuestBase
{
    public int CurrentGoalCount;
    public int GoalCount;
    [NonSerialized]
    public int TargetId;
    public MONSTER_ID MONSTER_ID;
    public QUEST_REWARD_TYPE QUEST_REWARD_TYPE;
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

    }
    public override void Progress(QuestMsgBase _questmsg)
    {
        if (_questmsg == null && _questmsg.GetQuestType() != QuestType)
            return;

        if(TargetId == _questmsg.GetDicMsgValue("TargetId"))
        {
            CurrentGoalCount++;
            //UnityEngine.Debug.Log(CurrentGoalCount + " - " + GoalCount);
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

    public override void Complete() //보상
    {
        switch(QUEST_REWARD_TYPE)
        {
            case QUEST_REWARD_TYPE.GOLD://골드보상지급
                {
                    ProgressPlayer.AddGold(Reward);
                    break;
                }
            case QUEST_REWARD_TYPE.EXP: //경험치보상
                {
                    ProgressPlayer.AddExp(Reward);
                    break;
                }
        }
       
        IsComplete = true;

        //완료 안내 메시지
        Shared.UiMgr.CreateSystemMsg(GetQusetName() + "완료!",SYSTEM_MSG_TYPE.QUEST_COMPLETE);
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
        string RewardTypeText = "";
        switch (QUEST_REWARD_TYPE)
        {
            case QUEST_REWARD_TYPE.GOLD://골드
                {
                    RewardTypeText = "<color=#FFD700><b>" + "GOLD" + "</b></color>";
                    break;
                }
            case QUEST_REWARD_TYPE.EXP: //경험치
                {
                    RewardTypeText = "<color=#ADFF2F><b>" + "EXP" + "</b></color>";
                    break;
                }
        }

        string TextReward = "<color=#00FF00>" + GetReward().ToString() + "</color>";
        string detail = RewardTypeText + " - "+ TextReward;
        return detail;
    }
}
