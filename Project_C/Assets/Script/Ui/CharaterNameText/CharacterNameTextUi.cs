using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacterNameTextUi : PoolAble
{
    [SerializeField]
    Image SpecialMark;
    QuestNPC Owner;
    string PrevIconName;
    public void Init(Character _owner)
    {
        QuestNPC npc = _owner as QuestNPC;
        if(npc != null )
        {
            Owner = npc;
        }

        PrevIconName = null;
        MarkUpdate();
    }
    public void SetMark(string _markName)
    {
        if(_markName != null)
        {
            SpecialMark.gameObject.SetActive(true);
            SpecialMark.sprite = Shared.GameMgr.GetSpriteAtlas("SpecialMark", _markName);
        }
        else
        {
            SpecialMark.gameObject.SetActive(false);
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
