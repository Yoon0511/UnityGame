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
        float damage = _damagedata.Damage - dicstat[STAT_TYPE.DEF];
        Shared.GameMgr.DAMAGE_FONT.CreateDamageFont(_damagedata, uihead.position);
        dicstat[STAT_TYPE.HP] = dicstat[STAT_TYPE.HP] - damage;
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
}
