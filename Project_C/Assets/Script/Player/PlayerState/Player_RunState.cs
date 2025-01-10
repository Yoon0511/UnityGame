
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RunState : StateBase
{
    Player player;
    public Player_RunState(Player _player)
    {
        player = _player;
    }

    public override void OnStateEnter()
    {
        player.PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.RUN);
    }

    public override void OnStateExit()
    {
        //Debug.Log("OnIdleExit");
    }

    public override void OnStateUpdate()
    {
        //Debug.Log("OnIdleUpdate");
    }
}
