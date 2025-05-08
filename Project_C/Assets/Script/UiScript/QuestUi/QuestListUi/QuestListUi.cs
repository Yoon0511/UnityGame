using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestListUi : MonoBehaviour
{
    public GameObject PARENT;
    public GameObject QUESTINFO;
    public GameObject HIDEEXPOSEBTN;

    [SerializeField]
    List<QuestInfo> ListQuestInfo = new List<QuestInfo>();
    List<QuestBase> ListPlayerQuest;
    private void Start()
    {
        ListPlayerQuest = Shared.GameMgr.PLAYER.GetProgressQusetList();
    }
    public void AddQuest(QuestBase _quest)
    {
        GameObject quest = Instantiate(QUESTINFO);
        quest.transform.SetParent(PARENT.transform);
        quest.GetComponent<QuestInfo>().Init(_quest);
        ListQuestInfo.Add(quest.GetComponent<QuestInfo>());

        HIDEEXPOSEBTN.SetActive(true);
    }

    public void Refresh()
    {
        List<QuestInfo> ListRemove = new List<QuestInfo>();

        foreach (QuestInfo _questinfo in ListQuestInfo)
        {
           foreach(QuestBase _playerquest in ListPlayerQuest)
            {
                if(_questinfo.GetQuest().GetId() == _playerquest.GetId())
                {
                    _questinfo.Refresh(_playerquest);

                    if(_playerquest.GetIsComplete())
                    {
                        ListRemove.Add(_questinfo);
                    }
                }
            }
        }

        for(int i = 0; i < ListRemove.Count;++i)
        {
            ListQuestInfo.Remove(ListRemove[i]);
            Destroy(ListRemove[i].gameObject);
        }

        if(ListQuestInfo.Count == 0)
        {
            HIDEEXPOSEBTN.SetActive(false);
        }
    }
}
