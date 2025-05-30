using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    protected int Gold;
    [SerializeField]
    Inventory Inventory;
    public Inventory GetInventory() { return Inventory; }
    void InventoryInit() { Inventory.SetOwner(this); }
    public int GetGold() { return Gold; }
    public void AddGold(int _gold) 
    { 
        Gold += _gold;
        Inventory.UpdateGoldText();
    }

    public bool UseGold(int _gold)
    {
        if (Gold < _gold)
            return false;

        Gold -= _gold;
        Inventory.UpdateGoldText();
        return true;
    }

    public void AddItem(int _itemid)
    {
        Inventory.AddItem(Shared.DataMgr.GetItem(_itemid));
        Inventory.UpdateGoldText();
    }

    public void OpenInventory()
    {
        Inventory.OpenUi();
    }
}
