using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PartyMember : PoolAble
{
    [SerializeField]
    Image Image;

    [SerializeField]
    Text Name;
    Player Player;
    public void Init(Player _player)
    {
        Player = _player;

        Name.text = _player.GetCharacterName();
    }

    private void FixedUpdate()
    {
        if (Player != null)
        {
            float CurrHp = Player.GetInStatData(STAT_TYPE.HP);
            float MaxHp = Player.GetInStatData(STAT_TYPE.MAXHP);
            Image.fillAmount = Mathf.Clamp01(CurrHp / MaxHp);
        }
    }
}
