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
        AttackCoolTime = 4.0f;
        Player.AutoAttack();
    }

    public override void OnStateExit()
    {

    }

    public override void OnStateUpdate()
    {
        ATtackElpsedTime += Time.deltaTime;

        if(ATtackElpsedTime >= AttackCoolTime)
        {
            ATtackElpsedTime = 0.0f;
            Player.AutoAttack();
        }
    }
}
