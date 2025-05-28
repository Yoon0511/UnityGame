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
    string[] Conversation;

    public abstract void Accept(); //����
    public abstract void Refusal(); //����
    public abstract void Complete(); //����Ʈ �Ϸ�
    public abstract void Fail(); //����Ʈ ����
    public abstract void Progress(QuestMsgBase _questmsg); //����Ʈ ������

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
    public string[] GetConversation() { return Conversation; }
    public void GiveQuestReward()
    {
        switch (QUEST_REWARD_TYPE)
        {
            case QUEST_REWARD_TYPE.GOLD://��庸������
                {
                    ProgressPlayer.AddGold(Reward);
                    break;
                }
            case QUEST_REWARD_TYPE.EXP: //����ġ����
                {
                    ProgressPlayer.AddExp(Reward);
                    break;
                }
        }
    }

    public string GetRewardTypeText()
    {
        string RewardTypeText = "";
        switch (QUEST_REWARD_TYPE)
        {
            case QUEST_REWARD_TYPE.GOLD://���
                {
                    RewardTypeText = "<color=#FFD700><b>" + "GOLD" + "</b></color>";
                    break;
                }
            case QUEST_REWARD_TYPE.EXP: //����ġ
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
        int count = _info.ConversationCount;
        Conversation = new string[count];
        Conversation = _info.Conversation.Split("-",StringSplitOptions.None);
    }
}