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
    public void Refresh()
    {
        player = Shared.GameMgr.PLAYER.GetComponent<Player>();

        NAME.text = "<color=#C0C0C0>" + player.GetCharacterName() + "</color>";
        ATK.text = "ATK : " + player.GetInStatData(STAT_TYPE.ATK);
        DEF.text = "DEF : " + player.GetInStatData(STAT_TYPE.DEF);
        SPEED.text = "SPEED : " + player.GetInStatData(STAT_TYPE.SPEED);
    }
}
