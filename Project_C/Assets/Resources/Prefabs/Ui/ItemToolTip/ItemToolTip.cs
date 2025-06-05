using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : PoolAble
{
    public RectTransform RectTransform;
    public Text ItemName;
    public Text ItemTypeText;
    public Text ItemExplanation;
    public Text PirceText;
    public Image ItemImg;
    public void Input(ItemBase _item)
    {
        transform.SetParent(Shared.GameMgr.CANVAS.transform, false);

        ItemImg.sprite = Shared.GameMgr.GetSpriteAtlas("Items", _item.SpriteName);
        ItemName.text = _item.GetItemName();
        ItemTypeText.text = _item.GetItemTypeText();
        ItemExplanation.text = _item.GetItemExplanation();
        PirceText.text = "ÆÇ¸Å°¡: " + _item.SellPrice.ToString();

    }
    public void SetPos(Vector2 _pos)
    {
        Vector2 pos = _pos;
        pos.x -= RectTransform.rect.width / 1.5f;
        RectTransform.position = pos;
    }
}
