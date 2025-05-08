using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDetails : MonoBehaviour
{
    public Text QUESTNAME;
    public Text QUESCONTENT;
    public Text QUESREWARD;

    public GameObject REWARDIMG;

    public void Init(QuestBase _quest)
    {
        QUESTNAME.text  = _quest.GetQusetName();
        QUESCONTENT.text = _quest.GetContents();
        QUESREWARD.text = _quest.GetRewardDetail();

        REWARDIMG.SetActive(true);
    }

    public void OnDisable()
    {
        QUESTNAME.text = "QUEST_NAME";
        QUESCONTENT.text = "QUEST_CONTENT";
        QUESREWARD.text = "QUEST_REWARD";

        REWARDIMG.SetActive(false);
    }
}
