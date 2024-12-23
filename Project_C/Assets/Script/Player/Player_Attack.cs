using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public void OnAttack()
    {
        if (curr_state == STATE.ATTACK)
        {
            return;
        }
        ChangeState(STATE.ATTACK);
    }
    public override void Hit(float damage)
    {
        float value = hp + (def - damage);
        hp = value;
        UpdateHpbar();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TAG_MONSTER"))
        {
            other.GetComponent<Monster>().Hit(atk);
        }
    }
}
