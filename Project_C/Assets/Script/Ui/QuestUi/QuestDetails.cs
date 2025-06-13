using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestDetails : MonoBehaviour
{
    public Text QUESTNAME;
    public Text QUESCONTENT;
    public Text QUESREWARD;

    public void Init(QuestBase _quest)
    {
        QUESTNAME.text  = "<color=#FFD700><b>" + _quest.GetQusetName() + "</b></color>";
        QUESCONTENT.text = "<color=#FFFFFF>" + _quest.GetContents() + "</color>";
        QUESREWARD.text = _quest.GetRewardDetail();
    }

    public void OnDisable()
    {
        QUESTNAME.text = "QUEST_NAME";
        QUESCONTENT.text = "QUEST_CONTENT";
        QUESREWARD.text = "QUEST_REWARD";
    }
}
