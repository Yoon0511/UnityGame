using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_RidingState : StateBase
{
    Player player;
    Riding Riding;
    float OrgSpeed;
    public Player_RidingState(Player _player)
    {
        player = _player;
        Riding = player.GetRiding();
    }

    public override void OnStateEnter()
    {
        OrgSpeed = player.Statdata.GetData(STAT_TYPE.SPEED);
        player.PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.RIDING);
    }

    public override void OnStateExit()
    {
        player.Statdata.SetStat(STAT_TYPE.SPEED, OrgSpeed);
    }

    public override void OnStateUpdate()
    {
        player.Statdata.SetStat(STAT_TYPE.SPEED, Riding.GetSpeedforState());
    }
}
