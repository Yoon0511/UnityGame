using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestInfo : MonoBehaviour
{
    public GameObject AutoMovePopupPrefap;
    public Text QUEST_NAME;
    public Text QUEST_CONTENTS;
    public Text QUEST_REWARD;

    string QuestNameText = "QuestName";
    string QuestContentsText = "QuestContents";
    string QuestRewardText = "Reward : ";

    QuestBase Quest;
    public void Init(QuestBase _quest)
    {
        Quest = _quest;
        QUEST_NAME.text = "<color=#FFD700><b>" + Quest.GetQusetName() + "</b></color>";
        QUEST_CONTENTS.text = Quest.GetContents();
        QUEST_REWARD.text = QuestRewardText + Quest.GetRewardDetail();
    }

    public void Refresh()
    {
        QUEST_CONTENTS.text = Quest.GetContents();
    }

    public void Refresh(QuestBase _quest)
    {
        QUEST_CONTENTS.text = _quest.GetContents();
    }
    public void QuestReset()
    {
        QUEST_NAME.text = QuestNameText;
        QUEST_CONTENTS.text = QuestContentsText;
        QUEST_REWARD.text = QuestRewardText;
    }

    public QuestBase GetQuest() { return Quest; }

    public void OnAutoMovePopup()
    {
        GameObject popup = Instantiate(AutoMovePopupPrefap);
        popup.transform.SetParent(Shared.GameMgr.CANVAS.transform, false);
    }
}
