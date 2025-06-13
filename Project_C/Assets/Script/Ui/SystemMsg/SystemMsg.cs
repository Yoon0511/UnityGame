using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SystemMsg : Particle
{
    [SerializeField]
    RectTransform RectTransform;
    [SerializeField]
    Text Text;

    float UiDuration = 1.0f;
    float QuestCompleteDuration = 2.5f;

   public void Init(string _msg,SYSTEM_MSG_TYPE _system_msg_type)
    {
        float y = 0;

        switch(_system_msg_type)
        {
            case SYSTEM_MSG_TYPE.UI:
                {
                    Duration = UiDuration;
                    y = -180;
                    break;
                }
            case SYSTEM_MSG_TYPE.QUEST_COMPLETE:
                {
                    Duration = QuestCompleteDuration;
                    y = 294;
                    break;
                }
        }

        transform.SetParent(Shared.GameMgr.CANVAS.transform, false);
        transform.SetAsLastSibling();  
        RectTransform.anchoredPosition = new Vector2(0, y);
        Text.text = "<color=#FFEB3B><b>" + _msg + "</b></color>";
    }
}
