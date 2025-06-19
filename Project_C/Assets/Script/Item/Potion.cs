using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : ItemBase
{
    public float Amount = 100f;
    public POTION_TYPE POTION_TYPE;
    public override void ItemUse()
    {
        switch(POTION_TYPE)
        {
            case POTION_TYPE.HP_POTION:
                {
                    Owner.EnhanceStat(STAT_TYPE.HP, Amount);
                    Shared.ParticleMgr.CreateParticle("Healing", Owner.transform, 1.0f);
                    break;
                }
            case POTION_TYPE.MP_POTION:
                {
                    Owner.EnhanceStat(STAT_TYPE.MP, Amount);
                    Shared.ParticleMgr.CreateParticle("HealingBlue", Owner.transform, 1.0f);
                    break;
                }
        }

        //사용시 삭제
        Shared.GameMgr.PLAYER.GetInventory().DeleteItem(this);

        Player Player;
        Owner.TryGetComponent<Player>(out Player);
        if(Player != null)
        {
            Shared.SoundMgr.PlaySFX("EAT_POTION");
        }
    }

    public void InputUseItemData(TableItem.UseItemInfo _info)
    {
        Amount = _info.Amount;
        POTION_TYPE = (POTION_TYPE)_info.PotionType;
    }

    public string GetPotionType()
    {
        switch(POTION_TYPE)
        {
            case POTION_TYPE.HP_POTION:
                return "<color=#FF4500>HP</color>";
            case POTION_TYPE.MP_POTION:
                return "<color=#1E90FF>MP</color>";
            default:
                return "NONE";
        }
    }

    public override string GetItemExplanation()
    {
        return $"{GetPotionType()}를 {Amount}회복합니다.";
    }
}
