using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenSlot : ItemSlot
{
    //Slot Ŭ�� �� ���Ծȿ� �ִ� �ش� �������� ����Ѵ�.
    public override void OnClickSlot()
    {
        if(Shared.GameMgr.PLAYER.GetUseStore()) // ���� �����
        {
            Shared.UiMgr.STORE.OnSellPopup(Item);
        }
        else
        {
            Item.ItemUse();
        }
    }
}
