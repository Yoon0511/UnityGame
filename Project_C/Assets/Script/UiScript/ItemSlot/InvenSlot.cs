using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenSlot : ItemSlot
{
    public override void OnClickSlot()
    {
        Item.ItemUse();
    }
}
