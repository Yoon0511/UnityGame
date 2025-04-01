using System.Collections;
using System.Collections.Generic;

public class HuntingMsg : QuestMsgBase
{
    public int TakeDamage;
    public int AtkDamage;
    public void SetMsg(int _takedmg,int _atkdamage,int _type, int _targetid, int _action)
    {
        DicMsg.Add("TakeDamage", _takedmg);
        DicMsg.Add("AtkDamage", _atkdamage);
        DicMsg.Add("Type", _type);
        DicMsg.Add("TargetId", _targetid);
        DicMsg.Add("Action", _action);

        Type = (int)QUEST_TYPE.HUNTING;
    }
}
