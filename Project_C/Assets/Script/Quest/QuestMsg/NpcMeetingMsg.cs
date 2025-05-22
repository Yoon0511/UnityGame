using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NpcMeetingMsg : QuestMsgBase
{
    public void SetMsg(int _questType,int _targetId)
    {
        Type = _questType;
        DicMsg.Add("Type", Type);
        DicMsg.Add("TargetId", _targetId);
    }
}
