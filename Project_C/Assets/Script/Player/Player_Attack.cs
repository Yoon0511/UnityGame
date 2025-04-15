using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    bool IsComboEnable = false;
    bool IsBasicAttack = true;

    int ComboIndex = 0;
    public void OnAttack()
    {
        if (CurrState != (int)STATE.ATTACK)
        {
            ChangeState((int)STATE.ATTACK);
        }
        //ChangeState((int)STATE.ATTACK, (int)PLAYER_ANI_STATE.ATTACK);

        if(IsBasicAttack)
        {
            PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.ATTACK);
        }

        if(IsComboEnable)
        {
            ComboIndex++;
            PlayAni_Trigger("Ani_SlashCombo");
            IsComboEnable = false;
        }
    }
    public override void Hit(float _damage)
    {
        Statdata.TakeDamage(_damage);
        UpdateHpbar();
    }

    public void OnTriggerEnter(Collider other)
    {
        //if (other.CompareTag("TAG_MONSTER"))
        //{
        //    other.GetComponent<Monster>().Hit(Statdata.GetData(STAT_TYPE.ATK));
        //}

        bool check = Shared.GameMgr.IsCheckCharacterType(other, (int)CHARACTER_TYPE.MONSTER);

        if (check)
        {
            float ComboAtk = 0.2f;
            float atk = Statdata.GetData(STAT_TYPE.ATK);

            atk = atk + (ComboAtk * ComboIndex * atk);
            
            //atk = Random.Range(1000, 9999);

            other.GetComponent<Character>().Hit(atk);
        }
    }
}
