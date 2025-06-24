using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
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
        if(PV.IsMine == false || IsDead)
        {
            return;
        }

        if (IsGuard)
        {
            GuardSuccess();
            return;
        }

        PV.RPC("RpcPlayerTakeDamage", RpcTarget.All, (float)_damagedata.Damage, (int)_damagedata.DamageFont_Type);
        //Statdata.TakeDamage(_damagedata);

        //rpc로 HP동기화
        
        UpdateHpbar();
    }

    [PunRPC]
    void RpcPlayerTakeDamage(float _damage, int _damagefontType)
    {
        DamageData damgedata = Shared.GameMgr.DamageDataPool.Get(_damage, (DAMAGEFONT_TYPE)_damagefontType);
        Statdata.TakeDamage(damgedata);

        PV.RPC("SyncHp", RpcTarget.All, GetInStatData(STAT_TYPE.HP));

        //사망
        if(GetInStatData(STAT_TYPE.HP) <= 0)
        {
            ChangeState((int)STATE.DIE);
        }
    }

    [PunRPC]
    void SyncHp(float _hp)
    {
        Statdata.SetStat(STAT_TYPE.HP, _hp);
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

            //사운드
            Shared.SoundMgr.PlaySFX("SWOARD_HIT");

            //other.GetComponent<Character>().Shake(0.1f, 0.1f);
            //카메라 흔들기
            //Shared.MainCamera.Shake(0,0f,0.1f,new Vector3(0.0f,2.0f,0.0f),5f,2f,1);
        }
    }
}
