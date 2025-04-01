using System.Collections;
using System.Collections.Generic;

public abstract class QuestBase
{
    protected string    QuestName;
    protected string    Contents;
    protected int       QuestType;
    protected int       Reward;
    protected bool      IsComplete = false;

    public abstract void Accept(); //����
    public abstract void Refusal(); //����
    public abstract void Complete(); //����Ʈ �Ϸ�
    public abstract void Fail(); //����Ʈ ����
    public abstract void Progress(QuestMsgBase _questmsg); //����Ʈ ������

    public void SetQusetName(string _qusetname) { QuestName = _qusetname; }
    public string GetQusetName() { return QuestName; }
    public void SetContents(string _contents) { Contents = _contents; }
    public string GetContents() { return Contents; }
    public void SetReward(int _reward) { Reward = _reward; }
    public int GetReward() { return Reward; }
    public void SetIsComplete(bool _iscomplete) { IsComplete = _iscomplete; }
    public bool GetIsComplete() {  return IsComplete; }
}

//msgbase -         atkmsg  /  converstationMsg / storemsg
//type           ->  atk,converstion,store 
//                   target(id)
//action         ->  atk,take /   start,end       / sell,buy