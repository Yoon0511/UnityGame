using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestUi : MonoBehaviour
{
    public GameObject CONTENT;
    public QuestDetails QUESTDETAILS;

    [SerializeField]
    List<GameObject> ListContent = new List<GameObject>();

    bool FirstOpen = true;
    QUESTUI_OPNE_TYPE QUESTUI_OPNE_TYPE;
    ToggleGroup ContentToggleGroup;
    private void Start()
    {
        ContentToggleGroup = CONTENT.GetComponent<ToggleGroup>();
    }
    public void OnEnable() // ����Ʈ UI Open�� ����
    {
        if(FirstOpen) //ó�� �����
        {
            FirstOpen = false;
            OnCanStartQuest(); //���۰��� ����Ʈ ǥ��
        }

        switch(QUESTUI_OPNE_TYPE) // �������� ���� �ִ� ����Ʈ ī�װ��� ������
        {
            case QUESTUI_OPNE_TYPE.CANSTART_QUEST:
                OnCanStartQuest();
                break;
            case QUESTUI_OPNE_TYPE.PROGRESS_QUEST:
                OnProgressQuest();
                break;
            case QUESTUI_OPNE_TYPE.COMPLETE_QUEST:
                OnCompleteQuest();
                break;
        }
    }
    public void OnCanStartQuest() // ���۰��� ����Ʈ
    {
        ContentReset();
        // ���۰����� ����Ʈ list�� �����´�.
        CatalogueInit(CreateStartQuest());
        QUESTUI_OPNE_TYPE = QUESTUI_OPNE_TYPE.CANSTART_QUEST;
    }

    public void OnProgressQuest() // �������� ����Ʈ
    {
        ContentReset();
        //Player�� �������� ����Ʈ list�� �����´�
        CatalogueInit(Shared.GameMgr.PLAYER.GetProgressQusetList()); 
        QUESTUI_OPNE_TYPE = QUESTUI_OPNE_TYPE.PROGRESS_QUEST;
    }

    public void OnCompleteQuest() // �Ϸ�� ����Ʈ
    {
        ContentReset();
        // Player�� �Ϸ�� ����Ʈ list�� �����´�.
        CatalogueInit(Shared.GameMgr.PLAYER.GetCompleteQuestList());
        QUESTUI_OPNE_TYPE = QUESTUI_OPNE_TYPE.COMPLETE_QUEST;
    }

    void CatalogueInit(List<QuestBase> _listquest)
    {
        if(ContentToggleGroup == null)
        {
            ContentToggleGroup = CONTENT.GetComponent<ToggleGroup>();
        }

        foreach (QuestBase Quest in _listquest)
        {
            GameObject CatalogueQuest = Shared.PoolMgr.GetObject("CatalogueQuest");
            CatalogueQuest.transform.SetParent(CONTENT.transform,false);
            CatalogueQuest.GetComponent<CatalogueQuest>().Init(Quest, QUESTDETAILS, ContentToggleGroup);
            ListContent.Add(CatalogueQuest);
        }
    }

    void ContentReset()
    {
        for(int i = ListContent.Count - 1; i >= 0; i--)
        {
            if (ListContent[i] != null)
            {
                PoolAble poolAble = ListContent[i].GetComponent<PoolAble>();
                if (poolAble != null)
                {
                    poolAble.ReleaseObject();
                }
            }
        }
        ListContent.Clear();
    }

    List<QuestBase> CreateStartQuest()
    {
        //int Count = Random.Range(4, 12);
        //
        //for(int i = 0;i< Count; ++i)
        //{
        //    QuestBase quest = new HuntingQuest();
        //    ((HuntingQuest)quest).Init(i, "��������Ʈ-" + i.ToString(), i.ToString() + " - ��������Ʈ",
        //        Random.Range(10, 30), i, Random.Range(500, 3000), null);
        //
        //    list.Add(quest);
        //}
        List<QuestBase> list = new List<QuestBase>();

        foreach (var pair in Shared.DataMgr.TableQuest.DicQuestBase)
        {
            list.Add(Shared.DataMgr.GetQuest(pair.Key));
        }

        HashSet<int> excludeQuestIds = new HashSet<int>();

        foreach (var quest in Shared.GameMgr.PLAYER.GetProgressQusetList())
        {
            excludeQuestIds.Add(quest.GetId());
        }

        foreach (var quest in Shared.GameMgr.PLAYER.GetCompleteQuestList())
        {
            excludeQuestIds.Add(quest.GetId());
        }

        for (int i = list.Count - 1; i >= 0; --i)
        {
            if (excludeQuestIds.Contains(list[i].GetId()))
            {
                list.RemoveAt(i);
            }
        }
        return list;
    }

    List<QuestBase> CreateProgessQuest()
    {
        List<QuestBase> list = new List<QuestBase>();
        int Count = Random.Range(4, 12);

        for (int i = 0; i < Count; ++i)
        {
            QuestBase quest = new HuntingQuest();
            ((HuntingQuest)quest).Init(i, "������-" + i.ToString(), i.ToString() + "- ������",
                Random.Range(10, 30), i, Random.Range(500, 3000), null);

            list.Add(quest);
        }
        return list;
    }
}
