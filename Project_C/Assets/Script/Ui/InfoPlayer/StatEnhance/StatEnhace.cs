using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatEnhace : MonoBehaviour
{
    public STAT_TYPE STAT_TYPE;
    public InfoPlayer InfoPlayer;
    public Text StatPointText;
    int StatPoint = 0;
    Player Player;

    private void Start()
    {
        Player = Shared.GameMgr.PLAYER;
    }

    public void OnStatAdd()
    {
        if(Player.MinusStatPoint(1))
        {
            InfoPlayer.DicStatEnhance[STAT_TYPE].value++;

            StatPoint = (int)InfoPlayer.DicStatEnhance[STAT_TYPE].value;
            StatPointText.text = StatPoint.ToString();

            InfoPlayer.Refresh();
        }
    }

    public void OnStatMinus()
    {
        StatPoint = (int)InfoPlayer.DicStatEnhance[STAT_TYPE].value;
        if(StatPoint <= 0)
        {
            return;
        }

        if (Player.AddStatPoint(1))
        {
            InfoPlayer.DicStatEnhance[STAT_TYPE].value--;

            StatPoint = (int)InfoPlayer.DicStatEnhance[STAT_TYPE].value;
            StatPointText.text = StatPoint.ToString();

            InfoPlayer.Refresh();
        }
    }

    public void Refresh()
    {
        StatPoint = (int)InfoPlayer.DicStatEnhance[STAT_TYPE].value;
        StatPointText.text = StatPoint.ToString();
    }
}
