using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CatalogueQuest : PoolAble
{
    public Text TEXT;
    public QuestDetails QUESTDETAILS;
    QuestBase Quest;
    public void Init(QuestBase _quest, QuestDetails _questdetails)
    {
        Quest = _quest;
        TEXT.text = _quest.GetQusetName();
        QUESTDETAILS = _questdetails;
    }

    public void OnQuestDetails()
    {
        QUESTDETAILS.Init(Quest);
    }
}
