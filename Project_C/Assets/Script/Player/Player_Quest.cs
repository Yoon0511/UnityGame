using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class Player
{
    [SerializeField]
    List<QuestBase> ListProgessQuest = new List<QuestBase>();
    List<QuestBase> ListCompleteQuest = new List<QuestBase>();

    public QuestListUi QUESTLISTUI;
    
    public void AddQuest(QuestBase _quest)
    {
        ListProgessQuest.Add(_quest);
        QUESTLISTUI.AddQuest(_quest);
    }

    public void RemoveQuest()
    {
        
    }

    public void RemoveQuest(QuestBase _quest)
    {
        ListProgessQuest.Remove(_quest);
    }

    public void RemoveQuestAll()
    {
        ListProgessQuest.Clear();
    }

    public List<QuestBase> GetProgressQusetList() { return ListProgessQuest; }
    public List<QuestBase> GetCompleteQuestList() { return ListCompleteQuest; }
    public void QusetProgress(QuestMsgBase _questmsg)
    {
        for(int i = 0;i< ListProgessQuest.Count;++i)
        {
            ListProgessQuest[i].Progress(_questmsg);
        }

        //foreach (QuestBase quest in ListProgessQuest)
        //{
        //    quest.Progress(_questmsg);
        //}

        QUESTLISTUI.Refresh(); //QuestUi 갱신        
    }

    public void QuestRefresh()
    {
        List<QuestBase> ListRemove = new List<QuestBase>();
        for (int i = 0; i < ListProgessQuest.Count; ++i)
        {
            if(ListProgessQuest[i].GetIsComplete())
            {
                ListCompleteQuest.Add(ListProgessQuest[i]); //완료된 퀘스트 리스트에 추가
                ListRemove.Add(ListProgessQuest[i]);
            }
        }

        //foreach (QuestBase quest in ListProgessQuest)
        //{
        //    if (quest.GetIsComplete())
        //    {
        //        ListCompleteQuest.Add(quest); //완료된 퀘스트 리스트에 추가
        //        ListRemove.Add(quest);
        //    }
        //}

        QUESTLISTUI.Refresh();

        for (int i = 0; i < ListRemove.Count; ++i)
        {
            RemoveQuest(ListRemove[i]);
        }
    }
}
