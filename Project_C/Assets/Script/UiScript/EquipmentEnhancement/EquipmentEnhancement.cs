using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentEnhancement : MonoBehaviour
{
    public GameObject CONTENT;
    public EnhanceView ENHANCEVIEW;

    List<GameObject> ListEnhaceEquipItemSlot = new List<GameObject>();
    Player Player;
    public void Init()
    {
        //장착중인 장비, 인벤에 있는 장비 가져와서 리스트 만들기
        if(Player == null)
        {
            Player = Shared.GameMgr.PLAYER;
        }

        foreach(EquipmentItem item in Player.GetDicEquitmentItem().Values)
        {
            CreateEnhaceEquipItemSlot(item);
        }

        foreach(ItemBase item in Player.GetInventory().GetItems())
        {
            if(item.ItemType == ITEM_TYPE.EQUIPMENT)
            {
                EquipmentItem equipment = item as EquipmentItem;
                if (equipment != null)
                {
                    CreateEnhaceEquipItemSlot(equipment);
                }
            }
        }

        float Count = ListEnhaceEquipItemSlot.Count;
        CONTENT.GetComponent<RectTransform>().sizeDelta = new Vector2(0, Count * 105.0f);
    }

    void CreateEnhaceEquipItemSlot(EquipmentItem _equipitem)
    {
        GameObject EnhanceEquipItemSlot = Shared.PoolMgr.GetObject("EnhanceEquipItemSlot");
        EnhanceEquipItemSlot.transform.SetParent(CONTENT.transform, false);
        EnhanceEquipItemSlot.GetComponent<EnhanceEquipItemSlot>().Init(_equipitem, ENHANCEVIEW);
        ListEnhaceEquipItemSlot.Add(EnhanceEquipItemSlot);
    }

    void EnhaceEquipSlotReset()
    {
        for (int i = ListEnhaceEquipItemSlot.Count - 1; i >= 0; i--)
        {
            if (ListEnhaceEquipItemSlot[i] != null)
            {
                PoolAble poolAble = ListEnhaceEquipItemSlot[i].GetComponent<PoolAble>();
                if (poolAble != null)
                {
                    poolAble.ReleaseObject();
                }
            }
        }
        ListEnhaceEquipItemSlot.Clear();
    }
}
