using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShowUi : MonoBehaviour
{
    public GameObject OPENUI;
    bool IsOpen = true;
    public void OnOpen()
    {
        OPENUI.SetActive(IsOpen);
        Shared.SoundMgr.PlaySFX("UI_NOTIFICATION");
    }
}
