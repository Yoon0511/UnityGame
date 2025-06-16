using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniMapIcon : PoolAble
{
    public Image Image;
    public RectTransform RectTransform;
    string ImgName;
    public void Init(int _type)
    {
        switch((CHARACTER_TYPE)_type)
        {
            case CHARACTER_TYPE.PLAYER:
                SetImage("Player");
                break;
            case CHARACTER_TYPE.MONSTER:
                SetImage("Monster");
                break;
            case CHARACTER_TYPE.NPC:
                SetImage("NPC");
                break;
            default:
                //SetImage("NPC");
                break;
        }
    }
    public void SetImage(string _imgname)
    {
        ImgName = _imgname;
        Image.sprite = Shared.GameMgr.GetSpriteAtlas("SpecialMark", _imgname);
    }

    public void SetPos(Vector2 pos)
    {
        RectTransform.localPosition = pos;
    }

    public void SetIconSize(int width, int height)
    {
        RectTransform.sizeDelta = new Vector2(width, height);
    }
    
    public string GetIconName()
    {
        return ImgName;
    }
}
