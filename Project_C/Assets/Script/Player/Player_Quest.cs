using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    //list�� ����
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
    

    //���,�ٸ�npc�� ��ȭ,��ǰ����
    //����Ʈ���� �÷��̾��� ���� �ٸ� �ൿ�� ���� ��
    //���� ����Ʈ ���ǿ� ���� �������� �Ǵ�

    //����������?

    //������ ��� ����

    //�÷��̾� �ൿ(���ݽ�,�������,NPC��ȭ��,��ǰ���Ž�) - ��� ��ȣ�ۿ��
    //�÷��̾� �ൿ������ ����Ʈ�� msg ����
    //����Ʈ�� msg�� �޾� ����Ʈ ���� �Ǵ�
}
