using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class QuestBase : MonoBehaviour
{
    [SerializeField]
    protected string    QuestName;
    [SerializeField]
    protected string    Contents;
    protected int       QuestType;
    [SerializeField]
    protected int       Reward;
    protected bool      IsComplete = false;
    protected NPC       OwnerNPC;
    protected Player    ProgressPlayer;
    [SerializeField]
    protected int       Id;
    [SerializeField]
    protected QUEST_REWARD_TYPE QUEST_REWARD_TYPE;
    string[] Conversation; // 시작대화
    string[] ProgressConversation; // 진행중 대화
    string[] CompleteConversation; // 완료 대화

    protected QUEST_STATE QuestState = QUEST_STATE.START;

    public abstract void Accept(); //수락
    public abstract void Refusal(); //거절
    public abstract void Complete(); //퀘스트 완료
    public abstract void Fail(); //퀘스트 실패
    public abstract void Progress(QuestMsgBase _questmsg); //퀘스트 진행중

    public void SetQusetName(string _qusetname) { QuestName = _qusetname; }
    public string GetQusetName() { return QuestName; }
    public void SetContents(string _contents) { Contents = _contents; }
    public abstract string GetContents();
    public void SetReward(int _reward) { Reward = _reward; }
    public int GetReward() { return Reward; }
    public void SetIsComplete(bool _iscomplete) { IsComplete = _iscomplete; }
    public bool GetIsComplete() {  return IsComplete; }
    public void SetOwnerNPC(NPC _owner) { OwnerNPC = _owner; }
    public NPC GetOwnerNPC() { return OwnerNPC; }
    public void SetProgressPlayer(Player _player) { ProgressPlayer = _player; } 
    public Player GetProgressPlayer() { return ProgressPlayer; }
    public int GetId() { return Id; }
    public abstract string GetRewardDetail();
    public QUEST_STATE GetQuestState() { return QuestState; }

    public void StateChange(QUEST_STATE _state)
    {
        QuestState = _state;
    }
    public string[] GetConversation() 
    {
        switch (QuestState)
        {
            case QUEST_STATE.START:
                return Conversation;
            case QUEST_STATE.PROGRESS:
                return ProgressConversation;
            case QUEST_STATE.COMPLETE:
                return CompleteConversation;
            default:
                return null;
        }
    }
    public void GiveQuestReward()
    {
        switch (QUEST_REWARD_TYPE)
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
        //Player 퀘스트 정보 갱신
        ProgressPlayer.QuestRefresh();

        //완료 안내 메시지
        Shared.UiMgr.CreateSystemMsg(GetQusetName() + "완료!", SYSTEM_MSG_TYPE.QUEST_COMPLETE);
    }

    public string GetRewardTypeText()
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
        return RewardTypeText;
    }

    public void InputQuestBaseData(TableQuest.QuestBaseInfo _info)
    {
        Id = _info.Id;
        QuestType = _info.Type;
        QuestName = _info.Name;
        Contents = _info.Contents;
        QUEST_REWARD_TYPE = (QUEST_REWARD_TYPE)_info.RewardType;
        Reward = _info.Reward;
        QuestState = QUEST_STATE.START;
        //시작대화
        int count = _info.Conversation.Split("-", StringSplitOptions.None).Length;
        Conversation = new string[count];
        Conversation = _info.Conversation.Split("-", StringSplitOptions.None);

        // 진행 중 대화
        count = _info.ProgressConversation.Split("-", StringSplitOptions.None).Length;
        ProgressConversation = new string[count];
        ProgressConversation = _info.ProgressConversation.Split("-", StringSplitOptions.None);

        // 완료 대화
        count = _info.CompleteConversation.Split("-", StringSplitOptions.None).Length;
        CompleteConversation = new string[count];
        CompleteConversation = _info.CompleteConversation.Split("-", StringSplitOptions.None);
    }

    public abstract void UpdateMiniMapIcon();
}