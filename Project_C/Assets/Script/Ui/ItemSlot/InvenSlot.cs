using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenSlot : ItemSlot
{
    //Slot 클릭 시 슬롯안에 있는 해당 아이템을 사용한다.
    public override void OnClickSlot()
    {
        if(Shared.GameMgr.PLAYER.GetUseStore()) // 상점 사용중
        {
            Shared.UiMgr.STORE.OnSellPopup(Item);
        }
        else
        {
            Item.ItemUse();
        }
    }
}
