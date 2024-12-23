using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public GameObject PLAYER;
    private void Awake()
    {
        Shared.GameMgr = this;
    }
}
