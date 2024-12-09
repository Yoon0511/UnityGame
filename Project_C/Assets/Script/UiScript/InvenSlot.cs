using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InvenSlot : MonoBehaviour
{
    Item item;
    public Image image;

    public void InputItem(Item _item)
    {
        if(_item == null)
        {
            item = null;
            image.sprite = null;
        }
        else
        {
            item = _item;
            image.sprite = item.ItemImage.sprite;
        }
    }

    public void DeleteItem()
    {
        item = null;
        image.sprite = null;
    }

    public Item GetSlotItem()
    {
        return item;
    }
}
