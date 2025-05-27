using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataMgr : MonoBehaviour
{
    private TableItem TableItem = new TableItem();
    private void Awake()
    {
        Shared.DataMgr = this;
    }

    private void Start()
    {
        TableItem.Init_CSV(1,0);
    }

    public ItemBase GetItem(int _id)
    {
        ItemBase item = null;

        // ItemType 备喊
        // 秦寸 Prefab 积己?
        // ItemType喊 蔼 积己
        //  - Weapon,Amor,Accessories
        //  - HPPotion,MPPotion
        return item;
    }
}
