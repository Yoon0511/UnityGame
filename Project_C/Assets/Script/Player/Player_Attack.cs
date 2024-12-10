using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public void OnAttack()
    {
        if (Curr_State == STATE.ATTACK)
        {
            return;
        }

        PlayAni_Trigger("Ani_ATK");
        ChangeState(STATE.ATTACK);
    }
    public override void Hit(float damage)
    {
        float value = Hp + (Def - damage);
        Hp = value;
        UpdateHpbar();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TAG_MONSTER"))
        {
            other.GetComponent<Monster>().Hit(Atk);
        }
    }
}
