using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnhanceView : MonoBehaviour
{
    public Image EQUIP_IMG;
    public Text EQUIP_NAME;
    public Text EQUIPSTAT_PREVIEW;
    public Text ENHANCE_MATERIALS;

    EquipmentItem Item;

    public void Init(EquipmentItem _item)
    {
        Item = _item;

        EQUIP_IMG.sprite = Shared.GameMgr.GetSpriteAtlas("Items", Item.SpriteName);
        EQUIP_NAME.text = "+" + Item.GetEnhanceValue().ToString() + " " + Item.ItemName;
        EQUIPSTAT_PREVIEW.text = Item.GetEnhanceInfo();
        ENHANCE_MATERIALS.text = "Gold = 5000";
    }

    public void OnTryEnhance()
    {
        Item.Enhance();
    }
}
