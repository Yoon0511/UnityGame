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
    Player player;
    public void Init(Player _player)
    {
        player = _player;

        Name.text = _player.GetCharacterName();
    }
}
