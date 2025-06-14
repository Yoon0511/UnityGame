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
        if(PV.IsMine == false)
        {
            return;
        }

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

    public override void Hit(DamageData _damagedata)
    {
        if (IsGuard)
        {
            GuardSuccess();
            return;
        }
        Statdata.TakeDamage(_damagedata);
        UpdateHpbar();
    }

    public void OnTriggerEnter(Collider other)
    {
        bool check = Shared.GameMgr.IsCheckCharacterType(other, (int)CHARACTER_TYPE.MONSTER);

        //기본공격
        if (check && HITBOX.activeSelf)
        {
            float ComboAtk = 0.2f; //기본공격계수
            float atk = Statdata.GetData(STAT_TYPE.ATK);

            atk = atk + (ComboAtk * ComboIndex * atk);

            //크리티컬 데미지 계산
            DamageData damgedata = Shared.GameMgr.DamageDataPool.Get(atk, DAMAGEFONT_TYPE.YELLOW);
            IsCritical(ref damgedata);
            other.GetComponent<Character>().Hit(damgedata);

            //파티클 생성
            Shared.ParticleMgr.CreateParticle("DarkHit", SLASH_TRAIL.transform, 0.6f);

            
            //other.GetComponent<Character>().Shake(0.1f, 0.1f);
            //카메라 흔들기
            //Shared.MainCamera.Shake(0,0f,0.1f,new Vector3(0.0f,2.0f,0.0f),5f,2f,1);
        }
    }
}
