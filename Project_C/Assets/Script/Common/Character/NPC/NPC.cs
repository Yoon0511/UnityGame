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

    protected CharacterNameText CharacterNameText;
    Quaternion OrgRotation;
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
    private void Awake()
    {
        CharacterNameText = GetComponent<CharacterNameText>();
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

    protected void SendQuestMsg()
    {
        NpcMeetingMsg MeetingMsg = new NpcMeetingMsg();
        MeetingMsg.SetMsg((int)QUEST_TYPE.MEETING, GetId());
        Shared.GameMgr.PLAYER.QusetProgress(MeetingMsg);
    }

    public virtual void UpdateMapIcon()
    {
        AllUpdateMapIcon("NPC", 5, 5);
        if (CharacterNameText.GetSpecialMark() != null)
        {
            SetCharacterSpecialMark(null);
        }
    }

    public void SetCharacterSpecialMark(string _markName)
    {
        if(CharacterNameText != null && CharacterNameText.GetSpecialMark() != null)
        {
            CharacterNameText.GetSpecialMark().SetMark(_markName);
        }
    }

    public void StartTalk()
    {
        OrgRotation = transform.rotation;

        //NPC�� �÷��̾ �ٶ󺸰� ȸ��
        Vector3 dir = Shared.GameMgr.PLAYER.transform.position - transform.position;
        dir.y = 0f; //���̰� ����
        if (dir != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(dir, Vector3.up);

        int Animation = (int)NPC_ANIMATION_STATE.TALK;
        PlayAnimation("Ani_State", Animation);
    }

    public void EndTalk()
    {
        transform.rotation = OrgRotation;

        int Animation = (int)NPC_ANIMATION_STATE.IDLE;
        PlayAnimation("Ani_State", Animation);
    }
}
