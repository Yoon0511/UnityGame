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
        CharacterName = "NPC_1";
        CharacterType = (int)CHARACTER_TYPE.NPC;
    }

    public override void RayTargetEvent()
    {
        
    }

    public override void UseSkill(int _index)
    {
        throw new System.NotImplementedException();
    }

}
