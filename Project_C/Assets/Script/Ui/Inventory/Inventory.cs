using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using SimpleJSON;

public class Inventory : MonoBehaviour, IPointerClickHandler
{
    public int NUM_MAX_ITEM = 25;
    public GameObject ITEMSLOT;
    public GameObject PARENTGRID;
    public Text GOLDTEXT;
    Character Owner;

    [SerializeField]
    List<ItemBase> items = new List<ItemBase>();

    [SerializeField]
    List<InvenSlot> slots = new List<InvenSlot>();

    [SerializeField]
    GameObject Ui;

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

    public void Refresh()
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

        UpdateGoldText();
    }

    public void AddItem(ItemBase _item)
    {
        if(items.Count < NUM_MAX_ITEM)
        {
            if (Owner != null)
            {
                _item.Owner = Owner;
            }
            items.Add(_item);
            Refresh();
        }
    }

    public void DeleteItem(ItemBase _item)
    {
        items.Remove(_item);
        //_item.ReleaseObject();
        Refresh();
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject obj = eventData.pointerCurrentRaycast.gameObject;
        InvenSlot slot = obj.transform.GetComponent<InvenSlot>();

        if (slot != null && slot.IsSlotItem() != null)
        {
            slot.OnClickSlot();
        }
    }

    public void SetOwner(Character _owner) { Owner = _owner; }
    public Character GetOwner() { return Owner; }
    public List<ItemBase> GetItems() { return items; }
    public void UpdateGoldText()
    {
        Player player = Owner as Player;
        if(player != null)
        {
            string Gold = player.GetGold().ToString();

            //π‡¿∫ ∞ÒµÂ
            GOLDTEXT.text = "<color=#FFD700><b>" + Gold + "</b></color>";
        }
    }

    public void OpenUi()
    {
        Ui.SetActive(true);
        Shared.SoundMgr.PlaySFX("UI_NOTIFICATION");
    }

    public InventoryJson ToJsonData()
    {
        InventoryJson json = new InventoryJson();

        Player player = Owner as Player;
        if (player != null)
        {
            json.Gold = player.GetGold();
        }

        foreach (ItemBase item in items)
        {
            json.ListItemId.Add(item.Id);
        }

        return json;
    }

    public void ApplyJsonData(InventoryJson _json)
    {
        for(int i = 0;i<_json.ListItemId.Count;++i)
        {
            int itemid = _json.ListItemId[i];
            AddItem(Shared.DataMgr.GetItem(itemid));
        }

        Player player = Owner as Player;
        if (player != null)
        {
            player.SetGold(_json.Gold);
        }
    }
}
