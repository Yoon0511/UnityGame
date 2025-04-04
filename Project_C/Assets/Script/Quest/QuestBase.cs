using System.Collections;
using System.Collections.Generic;

public abstract class QuestBase
{
    protected string    QuestName;
    protected string    Contents;
    protected int       QuestType;
    protected int       Reward;
    protected bool      IsComplete = false;

    public abstract void Accept(); //수락
    public abstract void Refusal(); //거절
    public abstract void Complete(); //퀘스트 완료
    public abstract void Fail(); //퀘스트 실패
    public abstract void Progress(QuestMsgBase _questmsg); //퀘스트 진행중

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