using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class AutoBtn : MonoBehaviour
{
    public Animator Animaotr;

    public void OnAutoMode()
    {
        Player Player = Shared.GameMgr.PLAYER;

        Player.OnSwitchAutoMode();
        Animaotr.SetBool("Play", Player.GetIsAutoMode());
    }
}
