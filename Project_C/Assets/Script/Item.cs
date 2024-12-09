using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public ITEM_TYPE ItemType;
    public string ItemName;
    public int ItemId;
    public Image ItemImage;

    public abstract void ItemUse();
}
