using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespwanPopup : MonoBehaviour
{
    public void OnRespwan()
    {
        Shared.GameMgr.PLAYER.Respwan();
        gameObject.SetActive(false);
    }
}
