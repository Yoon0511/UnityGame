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
        base.ItemUse(); //¿Â∫Ò ΩΩ∑‘ø° ¿Â¬¯

        //¿Â¬¯Ω√ ∆Í º“»Ø
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
        //∆Í ¡¶∞≈
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
        string explnation = $"<color=#FFFFFF>{GetStrSTAT_Type()}+</color> <color=#FF4500>{GetItemStat()}</color>";
        explnation += "\n";
        explnation += "PET ITEM";
        return explnation;
    }
}
