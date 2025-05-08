using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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

    public void OnEnable()
    {
        if(FirstOpen)
        {
            FirstOpen = false;
            OnCanStartQuest();
        }

        switch(QUESTUI_OPNE_TYPE)
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
    public void OnCanStartQuest()
    {
        ContentReset();
        CatalogueInit(CreateStartQuest());
        QUESTUI_OPNE_TYPE = QUESTUI_OPNE_TYPE.CANSTART_QUEST;
    }

    public void OnProgressQuest()
    {
        ContentReset();
        CatalogueInit(Shared.GameMgr.PLAYER.GetProgressQusetList());
        QUESTUI_OPNE_TYPE = QUESTUI_OPNE_TYPE.PROGRESS_QUEST;
    }

    public void OnCompleteQuest()
    {
        ContentReset();
        CatalogueInit(Shared.GameMgr.PLAYER.GetCompleteQuestList());
        QUESTUI_OPNE_TYPE = QUESTUI_OPNE_TYPE.COMPLETE_QUEST;
    }

    void CatalogueInit(List<QuestBase> _listquest)
    {
        foreach (QuestBase Quest in _listquest)
        {
            GameObject CatalogueQuest = Shared.PoolMgr.GetObject("CatalogueQuest");
            CatalogueQuest.transform.SetParent(CONTENT.transform,false);
            CatalogueQuest.GetComponent<CatalogueQuest>().Init(Quest, QUESTDETAILS);
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
        List<QuestBase> list = new List<QuestBase>();
        int Count = Random.Range(4, 12);

        for(int i = 0;i< Count; ++i)
        {
            QuestBase quest = new HuntingQuset();
            ((HuntingQuset)quest).Init(i, "시작퀘스트-" + i.ToString(), i.ToString() + " - 시작퀘스트",
                Random.Range(10, 30), i, Random.Range(500, 3000), null);

            list.Add(quest);
        }
        return list;
    }

    List<QuestBase> CreateProgessQuest()
    {
        List<QuestBase> list = new List<QuestBase>();
        int Count = Random.Range(4, 12);

        for (int i = 0; i < Count; ++i)
        {
            QuestBase quest = new HuntingQuset();
            ((HuntingQuset)quest).Init(i, "진행중-" + i.ToString(), i.ToString() + "- 진행중",
                Random.Range(10, 30), i, Random.Range(500, 3000), null);

            list.Add(quest);
        }
        return list;
    }
}
