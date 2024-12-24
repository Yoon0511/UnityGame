using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPlayer : MonoBehaviour
{
    public Text NAME;
    public Text ATK;
    public Text DEF;
    public Text SPEED;

    Player player;

    private void OnEnable()
    {
        Refresh();
    }
    void Refresh()
    {
        player = Shared.GameMgr.PLAYER.GetComponent<Player>();

        NAME.text = "<color=#C0C0C0>" + player.GetCharacterName() + "</color>";
        ATK.text = "ATK : " + player.GetStatData(STAT_TYPE.ATK);
        DEF.text = "DEF : " + player.GetStatData(STAT_TYPE.DEF);
        SPEED.text = "SPEED : " + player.GetStatData(STAT_TYPE.SPEED);
    }
}
