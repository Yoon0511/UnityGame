
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DieState : StateBase
{
    Player player;
    public Player_DieState(Player _player)
    {
        player = _player;
    }

    public override void OnStateEnter()
    {
        player.SetIsDead(true);
        Shared.UiMgr.RespwanPopup.gameObject.SetActive(true);
    }

    public override void OnStateExit()
    {
        player.SetIsDead(false);
        //Debug.Log("OnIdleExit");
    }

    public override void OnStateUpdate()
    {
        //Debug.Log("OnIdleUpdate");
    }
}
