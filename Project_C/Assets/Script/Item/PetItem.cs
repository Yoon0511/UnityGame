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
        base.ItemUse(); //장비 슬롯에 장착

        //장착시 펫 소환
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
        //펫 제거
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
        explnation += $"<color=#3498DB><b>방어력</b></color> <color=#FFFF66><b>버프</b></color>를 사용합니다.\n";
        explnation += $"<color=#F39C12><b>상승량</b> : (1 ~ 10)</color>\n";
        explnation += $"<color=#1ABC9C><b>지속시간</b> : (5 ~ 10)</color>초";
        return explnation;
    }
}
