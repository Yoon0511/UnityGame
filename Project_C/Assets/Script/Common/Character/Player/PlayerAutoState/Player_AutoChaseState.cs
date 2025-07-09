using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;

public class Player_AutoChaseState : StateBase
{
    Player Player;
    public Player_AutoChaseState(Player _player)
    {
        Player = _player;
    }
    public override void OnStateEnter()
    {
        Player.PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.RUN);
        Player.FindNearMonster();
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        Player.MoveToTarget();

        if(Player.IsMonsterInDetectionRange())
        {
            Player.ChangeState((int)AUTO_STATE.ATTACK);
        }
    }
}
