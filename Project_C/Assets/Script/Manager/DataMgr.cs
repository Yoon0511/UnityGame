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

        // ItemType ����
        // �ش� Prefab ����?
        // ItemType�� �� ����
        //  - Weapon,Amor,Accessories
        //  - HPPotion,MPPotion
        return item;
    }
}
