using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvenSlot : ItemSlot
{
    //Slot Ŭ�� �� ���Ծȿ� �ִ� �ش� �������� ����Ѵ�.
    public override void OnClickSlot()
    {
        Item.ItemUse();
    }
}
