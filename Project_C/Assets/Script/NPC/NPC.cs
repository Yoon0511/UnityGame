using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NPC : Character
{
    // PLAYER ray -> monster,player,npc 구분
    // NPC,PLAYER,MONSTER - Characher
    // NPC -> PartyPlayNPC,StoreNPC

    // Character => Type 추가(Player,Monster,NPC) ==> Type으로 구분?
    // Character를 상속받는 클래스로 구분?
    protected int Job;
    List<string> List_ConverstationTexts = new List<string>();
    public override void Fsm_Init()
    {
        throw new System.NotImplementedException();
    }

    public override void Hit(float _damage)
    {
        throw new System.NotImplementedException();
    }

    public override void Init()
    {
        CharacterName = "NPC_123";
        CharacterType = (int)CHARACTER_TYPE.NPC;
        List_ConverstationTexts.Clear();
        List_ConverstationTexts.Add("(start)1.Leave me alone.");
        List_ConverstationTexts.Add("2.goner until just a few minutes ago.");
        List_ConverstationTexts.Add("3.We are messengers from Vindale. Our journey ends");
        List_ConverstationTexts.Add("(end)4.haven't heard anything from him in some number of weeks.");
    }

    public List<string> GetConverstationTexts()
    {
        return List_ConverstationTexts;
    }
    public override void RayTargetEvent()
    {
        Debug.Log("NPC RayTargetEvent");
        Shared.GameMgr.OnNPCDialogueWindow(this);
    }

    public override void UseSkill(int _index)
    {
        throw new System.NotImplementedException();
    }

}
