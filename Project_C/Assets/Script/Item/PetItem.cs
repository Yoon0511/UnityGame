using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetItem : EquipmentItem
{
    public GameObject PetPrefab;
    Pet Pet;
    public override bool Enhance()
    {
        throw new System.NotImplementedException();
    }

    public override string GetEnhanceInfo()
    {
        throw new System.NotImplementedException();
    }

    public override float GetItemStat()
    {
        return DicEquipmentItemStat[STAT_TYPE.MAXHP];
    }

    public override string GetStrSTAT_Type()
    {
        return "MAXHP";
    }

    public override void ItemUse()
    {
        base.ItemUse(); //��� ���Կ� ����

        //������ �� ��ȯ
        Pet = Instantiate(PetPrefab).GetComponent<Pet>();
        if (Pet != null)
        {
            Owner.TryGetComponent<Player>(out Player Player);
            if (Player != null)
            {
                Player.EquipPet(Pet);
                Shared.ParticleMgr.CreateParticle("SpwanEffect", Pet.transform, 0.5f);
            }
        }
    }

    public override void UnEquip()
    {
        //�� ����
        if (Pet != null)
        {
            Owner.TryGetComponent<Player>(out Player Player);
            if (Player != null)
            {
                Player.UnEquipPet();
            }
        }
    }

    public override string GetItemExplanation()
    {
        string explnation = $"<color=#FF4C4C>{GetStrSTAT_Type()}+</color> <color=#FFFFFF>{GetItemStat()}</color>\n";
        explnation += $"<color=#3498DB><b>����</b></color> <color=#FFFF66><b>����</b></color>�� ����մϴ�.\n";
        explnation += $"<color=#F39C12><b>��·�</b> : (1 ~ 10)</color>\n";
        explnation += $"<color=#1ABC9C><b>���ӽð�</b> : (5 ~ 10)</color>��";
        return explnation;
    }
}
