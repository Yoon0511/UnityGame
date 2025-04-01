using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.Requests;
using UnityEngine;
using UnityEngine.UI;


public class QuestList : MonoBehaviour
{
    public Text QUEST_NAME;
    public Text QUEST_CONTENTS;
    public Text QUEST_REWARD;

    string QuestNameText = "QuestName";
    string QuestContentsText = "QuestContents";
    string QuestRewardText = "QuestReward";

    public void Init(QuestBase _quest)
    {
        QUEST_NAME.text = _quest.GetQusetName();
        QUEST_CONTENTS.text = _quest.GetContents();
        QUEST_REWARD.text = QuestRewardText + _quest.GetReward().ToString();
    }

    public void Refresh(string _contents)
    {
        QUEST_CONTENTS.text = _contents;
    }

    public void QuestReset()
    {
        QUEST_NAME.text = QuestNameText;
        QUEST_CONTENTS.text = QuestContentsText;
        QUEST_REWARD.text = QuestRewardText;
    }
}
