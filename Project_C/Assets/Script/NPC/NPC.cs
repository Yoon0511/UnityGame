using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class NPC : Character
{
    // PLAYER ray -> monster,player,npc ����
    // NPC,PLAYER,MONSTER - Characher
    // NPC -> PartyPlayNPC,StoreNPC

    // Character => Type �߰�(Player,Monster,NPC) ==> Type���� ����?
    // Character�� ��ӹ޴� Ŭ������ ����?
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
