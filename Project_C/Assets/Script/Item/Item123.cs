using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item123 : Item
{
    public override void ItemUse()
    {
        Debug.Log("HP_POTION - " + Id);
    }
}
