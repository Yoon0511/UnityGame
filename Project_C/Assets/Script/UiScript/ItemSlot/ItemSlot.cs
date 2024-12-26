using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class ItemSlot : MonoBehaviour
{
    protected Item item;
    public Image image;

    public void InputItem(Item _item)
    {
        if (_item == null)
        {
            item = null;
            image.sprite = null;
            SetImageAlpha(0f);
        }
        else
        {
            item = _item;
            image.sprite = item.img;
            SetImageAlpha(255f);
        }
    }

    public void DeleteItem()
    {
        item = null;
        image.sprite = null;
        SetImageAlpha(0f);
    }

    public Item IsSlotItem()
    {
        return item;
    }

    void SetImageAlpha(float _value)
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, _value);
    }

    public abstract void OnClickSlot();
}
