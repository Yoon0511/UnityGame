using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player
{
    [SerializeField]
    List<QuestBase> ListQuest = new List<QuestBase>();

    public QuestUi QuestUi;
    
    public void AddQuest(QuestBase _quest)
    {
        ListQuest.Add(_quest);
        QuestUi.AddQuest(_quest);
    }

    public void RemoveQuest()
    {
        
    }

    public void RemoveQuest(QuestBase _quest)
    {
        ListQuest.Remove(_quest);
    }

    public void RemoveQuestAll()
    {
        ListQuest.Clear();
    }

    public List<QuestBase> GetQusetList() { return ListQuest; }

    public void QusetProgress(QuestMsgBase _questmsg)
    {
        List<QuestBase> ListRemove = new List<QuestBase>();

        foreach (QuestBase quest in ListQuest)
        {
            quest.Progress(_questmsg);
            if(quest.GetIsComplete())
            {
                ListRemove.Add(quest);
            }
        }
        
        QuestUi.Refresh(); //QuestUi °»½Å

        for(int i = 0;i< ListRemove.Count;++i)
        {
            RemoveQuest(ListRemove[i]);
        }
    }
}
