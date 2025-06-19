using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBtn : MonoBehaviour
{
    public GameObject TARGET;

    public void OnExit()
    {
        TARGET.SetActive(false);
        Shared.SoundMgr.PlaySFX("UI_NOTIFICATION");
    }
}
