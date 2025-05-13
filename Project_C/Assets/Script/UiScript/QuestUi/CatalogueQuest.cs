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

    // Ŭ�� �� ����
    public void OnQuestDetails()
    {
        // �ش� ����Ʈ�� �ڼ��� ���� ǥ�ø� ���� QuestDetails�� Quest�� �����Ѵ�.
        QUESTDETAILS.Init(Quest);
        
        if(Toggle.isOn)
        {
            //Ŭ�� �� ��������� ����
            TEXT.text = "<color=#FFD700><b>" + Quest.GetQusetName() + "</b></color>";
        }
        else
        {
            //Ŭ�� ���� �� �ϴû����� ����
            TEXT.text = "<color=#66D9EF>" + Quest.GetQusetName() + "</color>";
        }
    }
}
