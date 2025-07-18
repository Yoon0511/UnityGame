using System.Collections;
using System.Collections.Generic;
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
        Player.FindNearMonster();

        //if (Player.GetAnimator().GetBool("Ani_IsSlashCombo") == true)
        //{
        //    Player.GetAnimator().SetBool("Ani_IsSlashCombo", false);
        //}
        Player.PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.RUN);
    }

    public override void OnStateExit()
    {
        
    }

    public override void OnStateUpdate()
    {
        if(Player.GetCurrAnimation() != (int)PLAYER_ANI_STATE.RUN)
        {
            Player.PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.RUN);
        }

        Player.MoveToTarget();

        if(Player.IsMonsterInDetectionRange())
        {
            Player.PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.IDLE);
            Player.ChangeState((int)AUTO_STATE.ATTACK);
        }
    }
}
