using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MiniMap
{
    public override void Init(GameObject _player)
    {
        base.Init(_player);
        IsRotate = false;
        IsZoom = false;
    }

    private void FixedUpdate()
    {
        if (Player == null)
        {
            return;
        }
        WorldMapUpdate();
    }
    public void WorldMapUpdate()
    {
        UpdatePlayerIcon();
        UpdateNPCIcon();
        UpdatePlayerCenterBox();
    }
}
