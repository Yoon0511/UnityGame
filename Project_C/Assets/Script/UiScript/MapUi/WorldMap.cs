using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldMap : MiniMap
{
    public override void Init()
    {
        IsRotate = false;
        IsZoom = false;
    }

    private void FixedUpdate()
    {
        WorldMapUpdate();
    }
    public void WorldMapUpdate()
    {
        UpdatePlayerIcon();
        UpdateNPCIcon();
        UpdatePlayerCenterBox();
    }
}
