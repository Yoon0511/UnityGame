using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceNPC : NPC
{
    public GameObject SERVICEUI;
    public override void RayTargetEvent(Character _character)
    {
        SERVICEUI.SetActive(true);
        //SERVICEUI.GetComponent<EquipmentEnhancement>().Init();
    }
}
