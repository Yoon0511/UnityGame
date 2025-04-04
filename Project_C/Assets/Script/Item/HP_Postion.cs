using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Postion : ItemBase
{
    public override void ItemUse()
    {
        Debug.Log("HP_POTION - " + Id);
    }
}
