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

    // 클릭 시 실행
    public void OnQuestDetails()
    {
        // 해당 퀘스트의 자세한 정보 표시를 위해 QuestDetails에 Quest를 전달한다.
        QUESTDETAILS.Init(Quest);
        
        if(Toggle.isOn)
        {
            //클릭 시 노란색으로 변경
            TEXT.text = "<color=#FFD700><b>" + Quest.GetQusetName() + "</b></color>";
        }
        else
        {
            //클릭 해제 시 하늘색으로 변경
            TEXT.text = "<color=#66D9EF>" + Quest.GetQusetName() + "</color>";
        }
    }
}
