using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AutoAttackState : StateBase
{
    Player Player;
    float AttackCoolTime;
    float ATtackElpsedTime;

    public Player_AutoAttackState(Player _player)
    {
        Player = _player;
    }

    public override void OnStateEnter()
    {

    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        Player.AutoAttack();

        if(Player.GetTargetCharacter().GetIsDead())
        {
            Player.ChangeState((int)AUTO_STATE.CHASE);
        }
    }

    
}
