using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NPC : Character
{
    [SerializeField]
    protected string Job;
    protected string[] ConverstationTexts;
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
        Shared.GameMgr.OnNPCDialogueWindow(this, (Player)_character);
    }

    public override void UseSkill(int _index)
    {

    }

    public override void Init()
    {
        CharacterName = NPC_NAME;
        CharacterType = (int)CHARACTER_TYPE.NPC;

        ConverstationTexts = new string[1];
        ConverstationTexts[0] = $"���� {Job} {GetOnlyCharacterName()}�Դϴ�.";
        //����Ʈ ���� - ���濹��
        //Quest = new HuntingQuset();
        //((HuntingQuset)Quest).Init(1,"GolemHunt", "Target Golem - ",5,(int)MONSTER_ID.GOLEM,500,this);
    }

    public virtual string[] GetConverstationTexts()
    {
        return ConverstationTexts;
    }

    public override string GetCharacterName()
    {
        //�ϴû� (�þ�)
        string Strjob = "<color=#00BFFF>" + Job + "</color>\n";
        //���� ���λ�
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
