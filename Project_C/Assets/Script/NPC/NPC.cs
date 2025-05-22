using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NPC : Character
{
    [SerializeField]
    protected string Job;
    List<string> List_ConverstationTexts = new List<string>();
    [SerializeField]
    protected string NPC_NAME;
    [SerializeField]
    protected int NPC_ID;

    public override void Fsm_Init()
    {
        
    }

    public override void Hit(DamageData _damagedata)
    {
        
    }
    public override void RayTargetEvent(Character _character)
    {
        SendQuestMsg();
    }

    public override void UseSkill(int _index)
    {

    }

    public override void Init()
    {
        CharacterName = NPC_NAME;
        CharacterType = (int)CHARACTER_TYPE.NPC;
        List_ConverstationTexts.Clear();
        List_ConverstationTexts.Add("(start)1.Leave me alone.");
        List_ConverstationTexts.Add("2.goner until just a few minutes ago.");
        List_ConverstationTexts.Add("3.We are messengers from Vindale. Our journey ends");
        List_ConverstationTexts.Add("(end)4.haven't heard anything from him in some number of weeks.");

        //퀘스트 생성 - 변경예정
        //Quest = new HuntingQuset();
        //((HuntingQuset)Quest).Init(1,"GolemHunt", "Target Golem - ",5,(int)MONSTER_ID.GOLEM,500,this);
    }

    public List<string> GetConverstationTexts()
    {
        return List_ConverstationTexts;
    }

    public override string GetCharacterName()
    {
        //하늘색 (시안)
        string Strjob = "<color=#00BFFF>" + Job + "</color>\n";
        //밝은 연두색
        string StrName = "<color=#7CFC00><b>" + base.GetCharacterName() + "</b></color>";
        string ColorJobName = Strjob + StrName;

        return ColorJobName;
    }

    public string GetOnlyCharacterName()
    {
        return CharacterName;
    }

    public void SetNPCID(int _ID) { NPC_ID = _ID; }
    public int GetNPCID() { return NPC_ID; }

    protected void SendQuestMsg()
    {
        NpcMeetingMsg MeetingMsg = new NpcMeetingMsg();
        MeetingMsg.SetMsg((int)QUEST_TYPE.MEETING, NPC_ID);
        Shared.GameMgr.PLAYER.QusetProgress(MeetingMsg);
    }
}
