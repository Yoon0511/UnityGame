
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_IdleState : StateBase
{
    Player player;
    public Player_IdleState(Player _player)
    {
        player = _player;
    }

    public override void OnStateEnter()
    {
        player.PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.IDLE);
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
