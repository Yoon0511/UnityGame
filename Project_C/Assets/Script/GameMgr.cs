using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMgr : MonoBehaviour
{
    public GameObject PLAYER;
    public CreateDamageText CREATE_DAMAGE_TEXT;
    private void Awake()
    {
        Shared.GameMgr = this;
    }
}
