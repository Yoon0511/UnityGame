using System.Collections;
using System.Collections.Generic;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class StatCompletePopup : MonoBehaviour
{
    public Text StatAmountOfChageText;
    Player Player;

    public void Init(InfoPlayer _infoPlayer)
    {
        if(Player == null)
        {
            Player = Shared.GameMgr.PLAYER;
        }

        string ChangeStat = null;
        for(int i = 0; i < (int)STAT_TYPE.ENUM_END; i++)
        {
            STAT_TYPE type = (STAT_TYPE)i;
            if (_infoPlayer.DicStatEnhance.TryGetValue(type, out STATENHACE stat))
            {
                if(_infoPlayer.DicStatEnhance[type].value <= 0)
                {
                    continue;
                }
                int orgStat = (int)Player.GetInStatData(type);
                int ChageStat = (int)Player.GetInStatData(type) + (int)(_infoPlayer.DicStatEnhance[type].value);
                ChangeStat += $"<color=#{Player.GetStatColor(type)}>{type.ToString()}</color> : {orgStat} -> <color=#FF0000><b>{ChageStat}</b></color>\n";
            }
        }

        StatAmountOfChageText.text = ChangeStat;
    }
   
}
