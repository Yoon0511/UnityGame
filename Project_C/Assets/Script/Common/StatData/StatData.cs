using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class StatData : MonoBehaviour
{
    [SerializeField]
    private SerializedDictionary<STAT_TYPE, float> dicstat;

    [SerializeField]
    Transform uihead;

    public void TakeDamage(DamageData _damagedata)
    {
        _damagedata.Damage = _damagedata.Damage - (int)dicstat[STAT_TYPE.DEF];
        Shared.GameMgr.DAMAGE_FONT.CreateDamageFont(_damagedata, uihead.position);
        dicstat[STAT_TYPE.HP] = dicstat[STAT_TYPE.HP] - _damagedata.Damage;
    }

    public void EnhanceStat(STAT_TYPE _type,float _value)
    {
        int type = (int) _type;
        dicstat[_type] += _value;
        switch (type)
        {
            case (int)STAT_TYPE.HP:
                dicstat[_type] = Mathf.Clamp(dicstat[_type], 0, dicstat[STAT_TYPE.MAXHP]);
                break;
            case (int)STAT_TYPE.MP:
                dicstat[_type] = Mathf.Clamp(dicstat[_type], 0, dicstat[STAT_TYPE.MAXMP]);
                break;
        }
    }

    public float GetData(STAT_TYPE _type)
    {
        return dicstat[_type];
    }

    public void SetStat(STAT_TYPE _type,float _value)
    {
        dicstat[_type] = _value;
    }

    public void AddExp(float _value)
    {
        dicstat[STAT_TYPE.EXP] += _value;

        if (dicstat[STAT_TYPE.EXP] >= dicstat[STAT_TYPE.MAXEXP])
        {
            dicstat[STAT_TYPE.EXP] = 0;
            dicstat[STAT_TYPE.LEVEL]++;
        }
    }

    public Transform GetUiHeadTransform() {return uihead;}
}
