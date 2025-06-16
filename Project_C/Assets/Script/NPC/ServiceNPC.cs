using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceNPC : NPC
{
    public GameObject SERVICEUI;
    public SERIVE_TYPE SERIVE_TYPE;
    public override void RayTargetEvent(Character _character)
    {
        SendQuestMsg();
        SERVICEUI.SetActive(true);
        //SERVICEUI.GetComponent<EquipmentEnhancement>().Init();
    }

    public override void UpdateMiniMapIcon()
    {
        SetMapIcon();
    }

    public override void UpdateMapIcon()
    {
        SetMapIcon();
    }

    private void SetMapIcon()
    {
        switch (SERIVE_TYPE)
        {
            case SERIVE_TYPE.ENHANCE:
                AllUpdateMapIcon("NPC_ENHANCE", 12, 12);
                //AllUpdateMapIcon("NPC_STORE", 12, 12);
                break;
            case SERIVE_TYPE.STORE:
                AllUpdateMapIcon("NPC_STORE", 12, 12);
                break;
        }
    }
}
