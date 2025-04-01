using System.Collections;
using System.Collections.Generic;

public class QuestMsgBase
{
    protected int Type;
    protected int TargetId;
    protected int Action;

    protected Dictionary<string,int> DicMsg = new Dictionary<string,int>();

    public int GetDicMsgValue(string _key)
    {
        return DicMsg[_key];
    }
    public int GetQuestType() { return Type; }
    public int GetTargetId() { return TargetId; }
    public int GetAction() { return Action; }
}
