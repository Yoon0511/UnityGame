using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestInfo : MonoBehaviour
{
    public Text QUEST_NAME;
    public Text QUEST_CONTENTS;
    public Text QUEST_REWARD;

    string QuestNameText = "QuestName";
    string QuestContentsText = "QuestContents";
    string QuestRewardText = "QuestReward : ";

    QuestBase Quest;
    public void Init(QuestBase _quest)
    {
        Quest = _quest;
        QUEST_NAME.text = Quest.GetQusetName();
        QUEST_CONTENTS.text = Quest.GetContents();
        QUEST_REWARD.text = QuestRewardText + Quest.GetReward().ToString();
    }

    public void Refresh()
    {
        QUEST_CONTENTS.text = Quest.GetContents();
    }

    public void QuestReset()
    {
        QUEST_NAME.text = QuestNameText;
        QUEST_CONTENTS.text = QuestContentsText;
        QUEST_REWARD.text = QuestRewardText;
    }
}
