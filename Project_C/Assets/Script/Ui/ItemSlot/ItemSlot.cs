using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public abstract class ItemSlot : PoolAble, IPointerEnterHandler, IPointerExitHandler
{
    public Image GradeFrame; // 아이템 등급 프레임
    public Image Image;
    protected ItemBase Item;
    ItemToolTip ItemToolTip;
    RectTransform RectTransform;

    public void Start()
    {
        RectTransform = GetComponent<RectTransform>();
    }
    public virtual void InputItem(ItemBase _Item)
    {
        Refresh(_Item);

        if (_Item == null)
        {
            Item = null;
            Image.sprite = null;
            SetImageAlpha(0f);
        }
        else
        {
            Item = _Item;
            Image.sprite = Shared.GameMgr.GetSpriteAtlas("Items", Item.SpriteName);
            SetImageAlpha(255f);
        }
    }

    public void DeleteItem()
    {
        Item = null;
        Image.sprite = null;
        SetImageAlpha(0f);
    }

    public ItemBase IsSlotItem()
    {
        return Item;
    }

    void SetImageAlpha(float _value)
    {
        Image.color = new Color(Image.color.r, Image.color.g, Image.color.b, _value);
    }
    public abstract void OnClickSlot();

    public override void ReleaseObject()
    {
        base.ReleaseObject();
        Item.ReleaseObject();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ItemToolTip == null)
        {
            if(Item != null)
            {
                ItemToolTip = Shared.PoolMgr.GetObject("ItemToolTip").GetComponent<ItemToolTip>();
                ItemToolTip.Input(Item);
                ItemToolTip.SetPos(eventData.position);
            }
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ItemToolTip != null)
        {
            //Debug.Log("out");
            ItemToolTip.ReleaseObject();
            ItemToolTip = null;
        }
    }

    public void Refresh(ItemBase _item)
    {
        if (_item == null)
        {
            Color color = new Color(1f, 1f, 1f, 0f); // 투명한 색상
            GradeFrame.color = color; // 프레임 색상 투명화
            return;
        }
        else
        {
            string Hexcolor = "#" + _item.GetStrGradeColor();
            if (ColorUtility.TryParseHtmlString(Hexcolor, out Color color))
            {
                color.a = 1f; // 불투명하게 설정
                GradeFrame.color = color;
            }
        }

    }
}
