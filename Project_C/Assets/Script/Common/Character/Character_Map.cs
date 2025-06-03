using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character
{
    [NonSerialized]
    public bool ShowMiniMapIcon = false;
    //protected MiniMapIcon MiniMapIcon = null;
    //public MiniMapIcon GetMiniMapIcon() { return MiniMapIcon; }
    //public void SetMiniMapIcon(MiniMapIcon _icon) { MiniMapIcon = _icon; }

    Dictionary<MiniMap, MiniMapIcon> DicMapIcons = new Dictionary<MiniMap, MiniMapIcon>();
    public virtual void UpdateMiniMapIcon()
    {
        var enumerator = DicMapIcons.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var kvp = enumerator.Current;
            MiniMap map = kvp.Key;
            MiniMapIcon icon = kvp.Value;

            icon.Init(GetCharacterType());
        }
    }

    public bool HasMapIcon()
    {
        return DicMapIcons.Count > 0;
    }
    public void SetMiniMapIcon(MiniMap _map, MiniMapIcon _icon)
    {
        DicMapIcons[_map] = _icon;
    }

    public MiniMapIcon GetMiniMapIcon(MiniMap _map)
    {
        if (DicMapIcons.TryGetValue(_map, out var icon))
            return icon;
        return null;
    }

    public void RemoveMiniMapIcon(MiniMap _map)
    {
        if (DicMapIcons.TryGetValue(_map, out var icon))
        {
            icon.ReleaseObject();
            DicMapIcons.Remove(_map);
        }
    }

    public void AllUpdateMapIcon(string _iconImgName,int _width,int _height)
    {
        var enumerator = DicMapIcons.GetEnumerator();
        while (enumerator.MoveNext())
        {
            var kvp = enumerator.Current;
            MiniMap map = kvp.Key;
            MiniMapIcon icon = kvp.Value;

            icon.SetImage(_iconImgName);
            icon.SetIconSize(_width, _height);
        }
    }
}
