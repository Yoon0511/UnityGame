using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour, IPointerClickHandler
{
    public int NUM_MAX_ITEM = 25;
    public GameObject ITEMSLOT;
    public GameObject PARENTGRID;

    [SerializeField]
    List<Item> items = new List<Item>();
    List<InvenSlot> slots = new List<InvenSlot>();

    void Awake()
    {
        if (items.Count == 0)
        {
            for (int i = 0; i < NUM_MAX_ITEM; ++i)
            {
                InvenSlot instSlot = Instantiate(ITEMSLOT, PARENTGRID.transform).GetComponent<InvenSlot>();
                slots.Add(instSlot);
            }
        }
        Refresh();
    }
    void Init()
    {
        if (items.Count == 0)
        {
            for (int i = 0; i < NUM_MAX_ITEM; ++i)
            {
                InvenSlot instSlot = Instantiate(ITEMSLOT, PARENTGRID.transform).GetComponent<InvenSlot>();
                slots.Add(instSlot);
            }
        }
        Refresh();
    }
    void Refresh()
    {
        int i = 0;
        for (; i < items.Count; ++i)
        {
            slots[i].InputItem(items[i]);
        }
        for(;i<NUM_MAX_ITEM;++i)
        {
            slots[i].InputItem(null);
        }
    }

    public void AddItem(Item _item)
    {
        if(items.Count < NUM_MAX_ITEM)
        {
            items.Add(_item);
            Refresh();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        InvenSlot slot = obj.transform.GetComponent<InvenSlot>();
        if (slot != null && slot.GetSlotItem() != null)
        {
            slot.GetSlotItem().ItemUse();
        }

    }
}
