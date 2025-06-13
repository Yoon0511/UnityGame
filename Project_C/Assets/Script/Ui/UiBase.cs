using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UiBase : MonoBehaviour
{
    protected Player Player;
    protected GameObject PlayerObj;
    public void SetPlayer(Player _player) {  Player = _player; }
}
