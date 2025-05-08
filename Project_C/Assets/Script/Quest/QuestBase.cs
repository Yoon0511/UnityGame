using System.Collections;
using System.Collections.Generic;

public abstract class QuestBase
{
    protected string    QuestName;
    protected string    Contents;
    protected int       QuestType;
    protected int       Reward;
    protected bool      IsComplete = false;
    protected NPC       OwnerNPC;
    protected Player    ProgressPlayer;
    protected int       Id;
    public abstract void Accept(); //수락
    public abstract void Refusal(); //거절
    public abstract void Complete(); //퀘스트 완료
    public abstract void Fail(); //퀘스트 실패
    public abstract void Progress(QuestMsgBase _questmsg); //퀘스트 진행중

    public void SetQusetName(string _qusetname) { QuestName = _qusetname; }
    public string GetQusetName() { return QuestName; }
    public void SetContents(string _contents) { Contents = _contents; }
    public abstract string GetContents();
    public void SetReward(int _reward) { Reward = _reward; }
    public int GetReward() { return Reward; }
    public void SetIsComplete(bool _iscomplete) { IsComplete = _iscomplete; }
    public bool GetIsComplete() {  return IsComplete; }
    public void SetOwnerNPC(NPC _owner) { OwnerNPC = _owner; }
    public NPC GetOwnerNPC() { return OwnerNPC; }
    public void SetProgressPlayer(Player _player) { ProgressPlayer = _player; } 
    public Player GetProgressPlayer() { return ProgressPlayer; }
    public int GetId() { return Id; }
    public abstract string GetRewardDetail();
}