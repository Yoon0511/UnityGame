using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    public void OnAttack()
    {
        if (CurrState == STATE.ATTACK)
        {
            return;
        }
        ChangeState(STATE.ATTACK, (int)PLAYER_ANI_STATE.ATTACK);
    }
    public override void Hit(float _damage)
    {
        Statdata.TakeDamage(_damage);
        UpdateHpbar();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TAG_MONSTER"))
        {
            other.GetComponent<Monster>().Hit(Statdata.GetData(STAT_TYPE.ATK));
        }
    }
}
