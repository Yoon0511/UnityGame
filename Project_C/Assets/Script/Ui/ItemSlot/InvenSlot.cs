using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.AI;
using UnityEngine.UI;

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
