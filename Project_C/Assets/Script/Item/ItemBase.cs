using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemBase : MonoBehaviour
{
    public ITEM_TYPE ItemType;
    public string ItemName;
    public int Id;
    public string SpriteName;

    public abstract void ItemUse();
}
