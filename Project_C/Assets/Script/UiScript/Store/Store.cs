using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Progress;

public class Store : MonoBehaviour
{
    public GameObject BUYPOPUP;
    public GameObject SELLPOPUP;
    public GameObject PARENTITEMLIST;
    public StoreItemSlot BUYPOPUP_ITEMSLOT;
    public StoreItemSlot SELLPOPUP_ITEMSLOT;
    List<int> ListItemId = new List<int>();

    List<StoreItemSlot> ListStoreItemSlot = new List<StoreItemSlot>();

    ItemBase SelectItem;

    // table연결필요
    int[] temp = { 1001, 1004, 1007, 1010, 1012, 1013,1016 };

    public void OnEnable()
    {
        Init();
        Shared.GameMgr.PLAYER.OpenInventory();
        Shared.GameMgr.PLAYER.SetUseStore(true);
    }

    public void OnDisable()
    {
        StroeItemSlotReset();
        Shared.GameMgr.PLAYER.SetUseStore(false);
    }

    public void Init()
    {
        //상점목록생성
        for (int i = 0; i < temp.Length; ++i)
        {
            StoreItemSlot storeItemSlot = Shared.PoolMgr.GetObject("StoreItemSlot").GetComponent<StoreItemSlot>();
            storeItemSlot.transform.SetParent(PARENTITEMLIST.transform, false);

            ItemBase item = Shared.DataMgr.GetItem(temp[i]);
            storeItemSlot.InputItem(item);
            storeItemSlot.Store = this;

            ListStoreItemSlot.Add(storeItemSlot);
        }
    }

    public void OnBuyPopup(ItemBase _item)
    {
        BUYPOPUP.SetActive(true);
        SelectItem = _item;
        BUYPOPUP_ITEMSLOT.InputItem(SelectItem);
    }

    public void OnBuy()
    {
        if(Shared.GameMgr.PLAYER.UseGold(SelectItem.BuyPrice))
        {
            //구매 성공
            Shared.GameMgr.PLAYER.AddItem(SelectItem.Id);
            Shared.UiMgr.CreateSystemMsg($"{SelectItem.ItemName}을 구매했습니다.", SYSTEM_MSG_TYPE.UI);
        }
        else //골드 부족으로 구매 실패
        {
            Shared.UiMgr.CreateSystemMsg("골드가 부족합니다.", SYSTEM_MSG_TYPE.UI);
        }

        BUYPOPUP.SetActive(false);
    }

    public void OnBuyCancel()
    {
        BUYPOPUP.SetActive(false);
    }

    public void StroeItemSlotReset()
    {
        for (int i = ListStoreItemSlot.Count-1; i >= 0;i--)
        {
            if(ListStoreItemSlot[i] != null)
            {
                ListStoreItemSlot[i].ReleaseObject();
            }
        }
        ListStoreItemSlot.Clear();
    }

    public void OnSellPopup(ItemBase _item)
    {
        SELLPOPUP.SetActive(true);
        SelectItem = _item;
        SELLPOPUP_ITEMSLOT.InputItem(SelectItem);
    }

    public void OnSell()
    {
        Shared.GameMgr.PLAYER.SellItem(SelectItem);
        SELLPOPUP.SetActive(false);
    }

    public void OnSellCancle()
    {
        SELLPOPUP.SetActive(false);
    }
}
