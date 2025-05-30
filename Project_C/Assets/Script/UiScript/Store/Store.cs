using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Store : MonoBehaviour
{
    public GameObject POPUP;
    public GameObject PARENTITEMLIST;
    public StoreItemSlot POPUP_ITEMSLOT;
    List<int> ListItemId = new List<int>();

    List<StoreItemSlot> ListStoreItemSlot = new List<StoreItemSlot>();

    ItemBase SelectItem;

    // table연결필요
    int[] temp = { 1001, 1004, 1007, 1010, 1012, 1013,1016 };

    public void OnEnable()
    {
        Init();
        Shared.GameMgr.PLAYER.OpenInventory();
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

    public void OnPopup(ItemBase _item)
    {
        POPUP.SetActive(true);
        SelectItem = _item;
        POPUP_ITEMSLOT.InputItem(SelectItem);
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

        POPUP.SetActive(false);
    }

    public void OnCancel()
    {
        POPUP.SetActive(false);
    }

    public void OnDisable()
    {
        StroeItemSlotReset();
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
}
