using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public partial class Player
{
    [SerializeField]
    GameObject HITBOX;

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
        ChangeState(prev_state);
    }
}
