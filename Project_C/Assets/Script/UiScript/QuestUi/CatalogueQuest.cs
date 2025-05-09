using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;

public class CatalogueQuest : PoolAble
{
    public Text TEXT;
    public QuestDetails QUESTDETAILS;
    QuestBase Quest;
    Toggle Toggle;
    public void Init(QuestBase _quest, QuestDetails _questdetails, ToggleGroup _toggleGroup)
    {
        if(Toggle == null)
        {
            Toggle = GetComponent<Toggle>();
        }
        Toggle.group = _toggleGroup;
        Quest = _quest;
        TEXT.text = "<color=#66D9EF>" + Quest.GetQusetName() + "</color>";
        QUESTDETAILS = _questdetails;
    }

    public void OnQuestDetails()
    {
        QUESTDETAILS.Init(Quest);
        
        if(Toggle.isOn)
        {
            TEXT.text = "<color=#FFD700><b>" + Quest.GetQusetName() + "</b></color>";
        }
        else
        {
            TEXT.text = "<color=#66D9EF>" + Quest.GetQusetName() + "</color>";
        }
    }
}
