using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TogleScript : MonoBehaviour
{
    public GameObject PLAYER;
    public GameObject MONSTER;
    public GameObject PLAYER_TOGLE;
    public GameObject MONSTER_TOGLE;

    bool OnPlayer = false;
    bool OnMonster = false;

    public void OnActivePlayer()
    {
        bool IsOn = PLAYER_TOGLE.GetComponent<Toggle>().isOn;
        PLAYER.SetActive(IsOn);
        OnPlayer = !OnPlayer;
    }

    public void OnActiveMonster()
    {
        bool IsOn = MONSTER_TOGLE.GetComponent<Toggle>().isOn;
        MONSTER.SetActive(IsOn);
        OnMonster = !OnMonster;
    }

    public void Index_OnActiveMonster(int _Index)
    {
        Debug.Log(_Index);
    }
}
