using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public ITEM_TYPE itemtype;
    public string itemname;
    public int id;
    public Sprite img;

    public abstract void ItemUse();
}
