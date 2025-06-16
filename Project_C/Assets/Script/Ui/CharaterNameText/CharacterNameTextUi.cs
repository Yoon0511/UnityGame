using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.GridLayoutGroup;

public class CharacterNameTextUi : PoolAble
{
    [SerializeField]
    Image SpecialMark;
    Character Owner;
    string PrevIconName;
    public void Init(Character _owner)
    {
        Owner = _owner;
        PrevIconName = null;
        MarkUpdate();
    }
    public void SetMark(string _markName)
    {
        if(_markName != null)
        {
            SpecialMark.color = new Color(255f, 255f, 255f, 255f);
            SpecialMark.gameObject.SetActive(true);
            SpecialMark.sprite = Shared.GameMgr.GetSpriteAtlas("SpecialMark", _markName);
        }
        else
        {
            SpecialMark.color = new Color(255f, 255f, 255f, 0f);
        }
    }
    public void Update()
    {
        if( Owner != null )
        {
            MarkUpdate();
        }
    }
    public void MarkUpdate()
    {
        if(Owner == null)
        {
            return;
        }

        string IconName = Owner.GetMiniMapIcon(Shared.UiMgr.MiniMap).GetIconName();

        if(PrevIconName == IconName)
        {
            return;
        }
        PrevIconName = IconName;

        switch(IconName)
        {
            case "Exclamation_mark":
            case "Question_mark":
                SetMark(IconName);
                break;
            default:
                SetMark(null);
                break;
        }
    }
}
