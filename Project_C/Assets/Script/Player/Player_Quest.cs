using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    //list·Î º¯°æ
    QuestBase Quest;
    //QuestUi
    public QuestInfo QuestInfo;

    List<QuestBase> ListQuest = new List<QuestBase>();
    
    public void AddQuest(QuestBase _quest)
    {
        Quest = _quest;
        //ListQuest.Add(_quest);

        QuestInfo.Init(_quest);
    }

    public void RemoveQuest()
    {
        Quest = null;
    }

    public void RemoveQuest(QuestBase _quest)
    {
        ListQuest.Remove(_quest);
    }

    public void RemoveQuestAll()
    {
        ListQuest.Clear();
    }

    public QuestBase GetCurrentQuest() { return Quest; }
    public List<QuestBase> GetQusetList() { return ListQuest; }

    public void QusetProgress(QuestMsgBase _questmsg)
    {
        Quest.Progress(_questmsg);
        QuestInfo.Refresh();

        if (Quest.GetIsComplete())
        {
            RemoveQuest();
            QuestInfo.QuestReset();
        }

        //foreach (QuestBase _quest in ListQuest)
        //{
        //    _quest.Progress(_questmsg);
        //}
    }
}
