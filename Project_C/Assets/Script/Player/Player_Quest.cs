using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    //list로 변경
    QuestBase Quest;
    //QuestUi
    public QuestList QuestList;

    List<QuestBase> ListQuest = new List<QuestBase>();
    
    public void AddQuest(QuestBase _quest)
    {
        Quest = _quest;
        //ListQuest.Add(_quest);

        QuestList.Init(_quest);
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

        if(Quest.GetIsComplete())
        {
            RemoveQuest();
            QuestList.QuestReset();
        }

        //foreach (QuestBase _quest in ListQuest)
        //{
        //    _quest.Progress(_questmsg);
        //}
    }
    

    //사냥,다른npc와 대화,물품구매
    //퀘스트마다 플레이어의 각기 다른 행동을 감지 후
    //각각 퀘스트 조건에 따라 성공여부 판단

    //옵저버패턴?

    //감지를 어떻게 할지

    //플레이어 행동(공격시,몹사망시,NPC대화시,물품구매시) - 모든 상호작용시
    //플레이어 행동에따라 퀘스트에 msg 전송
    //퀘스트는 msg를 받아 퀘스트 조건 판단
}
