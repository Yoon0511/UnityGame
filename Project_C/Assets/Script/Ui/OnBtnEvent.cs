using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnBtnEvent : MonoBehaviour
{
    public Player PLAYER;
    // Start is called before the first frame update
    public void OnPlayerAttack()
    {
        PLAYER.OnAttack();
    }
}
