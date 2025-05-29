using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static TableCharacter;
using static TableItem;

public class TableQuest : TableBase
{
    [Serializable]
    public class QuestBaseInfo
    {
        public int Id;
        public byte Type;
        public string Name;
        public string Contents;
        public byte RewardType;
        public int Reward;
        public string Conversation;
        public string ProgressConversation;
        public string CompleteConversation;
    }

    [Serializable]
    public class HuntingQuestInfo
    {
        public int Id;
        public int TargetId;
        public int CurrentGoalCount;
        public int GoalCount;
    }

    [Serializable]
    public class NPCMeetingQuestInfo
    {
        public int Id;
        public int TargetNPCId;
        public string TargetNPCName;
    }

    string[] ItemFile = { "Quest(QuestBase)", "Quest(HuntingQuest)", "Quest(NPCMeetingQuest)"};
    enum File
    {
        QuestBase,
        HuntingQuest,
        NPCMeetingQuest,
    }

    public Dictionary<int, QuestBaseInfo> DicQuestBase = new Dictionary<int, QuestBaseInfo>();
    public Dictionary<int, HuntingQuestInfo> DicHuntingQuest = new Dictionary<int, HuntingQuestInfo>();
    public Dictionary<int, NPCMeetingQuestInfo> DicNPCMeetingQuest = new Dictionary<int, NPCMeetingQuestInfo>();

    public void Init_CSV(int _Row, int _Col)
    {
        // QuestBase
        CSVReader reader = GetCSVReader(ItemFile[(int)File.QuestBase]);

        for (int row = _Row; row < reader.row; ++row)
        {
            QuestBaseInfo info = new QuestBaseInfo();

            if (Read(reader, info, row, _Col) == false)
                break;

            DicQuestBase.Add(info.Id, info);
        }

        // HuntingQuest
        reader = GetCSVReader(ItemFile[(int)File.HuntingQuest]);

        for (int row = _Row; row < reader.row; ++row)
        {
            HuntingQuestInfo info = new HuntingQuestInfo();

            if (Read(reader, info, row, _Col) == false)
                break;

            DicHuntingQuest.Add(info.Id, info);
        }

        //NPCMeetingQuest
        reader = GetCSVReader(ItemFile[(int)File.NPCMeetingQuest]);

        for (int row = _Row; row < reader.row; ++row)
        {
            NPCMeetingQuestInfo info = new NPCMeetingQuestInfo();

            if (Read(reader, info, row, _Col) == false)
                break;

            DicNPCMeetingQuest.Add(info.Id, info);
        }
    }

    // QuestBase
    protected bool Read(CSVReader _Reader, QuestBaseInfo _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.get(_Row, ref _Info.Id);
        _Reader.get(_Row, ref _Info.Type);
        _Reader.get(_Row, ref _Info.Name);
        _Reader.get(_Row, ref _Info.Contents);
        _Reader.get(_Row, ref _Info.RewardType);
        _Reader.get(_Row, ref _Info.Reward);
        _Reader.get(_Row, ref _Info.Conversation);
        _Reader.get(_Row, ref _Info.ProgressConversation);
        _Reader.get(_Row, ref _Info.CompleteConversation);

        return true;
    }

    // HuntingQuest
    protected bool Read(CSVReader _Reader, HuntingQuestInfo _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.get(_Row, ref _Info.Id);
        _Reader.get(_Row, ref _Info.TargetId);
        _Reader.get(_Row, ref _Info.CurrentGoalCount);
        _Reader.get(_Row, ref _Info.GoalCount);

        return true;
    }
    // NPCMeetingQuest
    protected bool Read(CSVReader _Reader, NPCMeetingQuestInfo _Info, int _Row, int _Col)
    {
        if (_Reader.reset_row(_Row, _Col) == false)
            return false;

        _Reader.get(_Row, ref _Info.Id);
        _Reader.get(_Row, ref _Info.TargetNPCId);
        _Reader.get(_Row, ref _Info.TargetNPCName);

        return true;
    }
    public void PrintQuestBaseInfo()
    {
        if (DicQuestBase.Count == 0)
        {
            Debug.Log("DicQuestBase is empty.");
            return;
        }

        foreach (var pair in DicQuestBase)
        {
            QuestBaseInfo info = pair.Value;
            string log = $"[Quest ID: {info.Id}]\n" +
                         $"- Type: {info.Type}\n" +
                         $"- Name: {info.Name}\n" +
                         $"- Contents: {info.Contents}\n" +
                         $"- RewardType: {info.RewardType}\n" +
                         $"- Reward: {info.Reward}\n" +
                         $"- Conversation: {info.Conversation}\n" +
                         $"- ProgressConversation: {info.ProgressConversation}\n" +
                         $"- CompleteConversation: {info.CompleteConversation}\n";

            Debug.Log(log);
        }

        //string[] con = DicQuestBase[2001].Conversation.Split("-",StringSplitOptions.None);
        //for(int i =0;i<con.Length;i++)
        //{
        //    Debug.Log(con[i]);
        //}
    }
    public QuestBaseInfo GetQuestBaseInfo(int _id)
    {
        if (DicQuestBase.TryGetValue(_id, out var info))
            return info;

        Debug.LogWarning($"[DataMgr] QuestBaseInfo ID {_id}가 존재하지 않습니다.");
        return null;
    }
    public HuntingQuestInfo GetHuntingQuestInfo(int _id)
    {
        if (DicHuntingQuest.TryGetValue(_id, out var info))
            return info;

        Debug.LogWarning($"[DataMgr] HuntingQuestInfo ID {_id}가 존재하지 않습니다.");
        return null;
    }
    public NPCMeetingQuestInfo GetNPCMeetingQuestInfo(int _id)
    {
        if (DicNPCMeetingQuest.TryGetValue(_id, out var info))
            return info;

        Debug.LogWarning($"[DataMgr] NPCMeetingQuestInfo ID {_id}가 존재하지 않습니다.");
        return null;
    }

    public void PrintHuntingQuest()
    {
        Debug.Log($"[HuntingQuest] Total Quests: {DicHuntingQuest.Count}");

        foreach (KeyValuePair<int, HuntingQuestInfo> pair in DicHuntingQuest)
        {
            HuntingQuestInfo info = pair.Value;
            Debug.Log($"ID: {info.Id}, TargetId: {info.TargetId}, CurrentGoalCount: {info.CurrentGoalCount}, GoalCount: {info.GoalCount}");
        }
    }

    public void PrintNPCMeetingQuest()
    {
        Debug.Log($"[NPCMeetingQuest] Total Quests: {DicNPCMeetingQuest.Count}");

        foreach (KeyValuePair<int, NPCMeetingQuestInfo> pair in DicNPCMeetingQuest)
        {
            NPCMeetingQuestInfo info = pair.Value;
            Debug.Log($"ID: {info.Id}, TargetNPCId: {info.TargetNPCId}");
        }
    }
}
