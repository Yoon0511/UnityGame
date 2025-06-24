using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_AutoMoveState : StateBase
{
    Player Player;
    float SystemMsgCooltime;
    float SystemMsgElapsedTime = 0.0f;

    Text SystemMsg;
    int DotCount = 0;
    public Player_AutoMoveState(Player _player)
    {
        Player = _player;
        SystemMsg = Shared.UiMgr.SystemMsg;
    }

    public override void OnStateEnter()
    {
        DotCount = 0;
        SystemMsg.gameObject.SetActive(true);
        SystemMsgDotAni();
        SystemMsgCooltime = 0.5f;
        SystemMsgElapsedTime = 0.0f;

        Player.SetIsAutoMove(true);
        Player.AutoMovePathInit();

        AutoMoveAnimation();
    }

    public override void OnStateExit()
    {
        SystemMsg.gameObject.SetActive(false);
        Player.SetIsAutoMove(false);
    }

    public override void OnStateUpdate()
    {
        Player.PathNodeUpdate();

        if (Player.GetIsPathComplete())
        {
            Player.SetIsAutoMove(false);
            Player.ChangeState(Player.GetPrevState());
        }

        SystemMsgElapsedTime += Time.deltaTime;
        if (SystemMsgElapsedTime >= SystemMsgCooltime)
        {
            SystemMsgElapsedTime = 0.0f;
            SystemMsgDotAni();
        }
    }

    void AutoMoveAnimation()
    {
        if(Player.GetIsRiding())
        {
            Player.GetRiding().ChangeState((int)RIDING_STATE.RUN);
        }
        else
        {
            Player.PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.RUN);
        }
    }

    void SystemMsgDotAni()
    {
        DotCount = (DotCount + 1) % 4; // 0~3
        string msg = "자동이동중" + new string('.', DotCount);
        SystemMsg.text = $"<color=#FFEB3B><b>{msg}</b></color>";
    }
}
