using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_AniEvent : MonoBehaviour
{
    [SerializeField]
    Player PLAYER;

    [SerializeField]
    GameObject HITBOX;

    public void Start()
    {
        if(PLAYER == null)
        {
            PLAYER = this.GetComponentInParent<Player>();
        }
    }

    public void OnHitBoxActive()
    {
        HITBOX.SetActive(true);
    }

    public void OffHitBoxActive()
    {
        HITBOX.SetActive(false);
    }

    public void OnAttackAniEnd()
    {
        PLAYER.AniATKEnd();
    }
}
