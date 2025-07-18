using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class StatData : MonoBehaviour
{
    [SerializeField]
    private SerializedDictionary<STAT_TYPE, float> dicstat;

    [SerializeField]
    private SerializedDictionary<STAT_TYPE, float> DicStatBonus;

    [SerializeField]
    Transform uihead;

    private void Start()
    {
        //DicStatBonus 초기화
        for(STAT_TYPE i = STAT_TYPE.HP;i<STAT_TYPE.ENUM_END;++i)
        {
            DicStatBonus.Add(i, 0);
        }
    }
    public void TakeDamage(DamageData _damagedata)
    {
        _damagedata.Damage = _damagedata.Damage - (int)dicstat[STAT_TYPE.DEF];
        Shared.GameMgr.DAMAGE_FONT.CreateDamageFont(_damagedata, uihead.position);
        dicstat[STAT_TYPE.HP] = dicstat[STAT_TYPE.HP] - _damagedata.Damage;
    }

    public void EnhanceStat(STAT_TYPE _type,float _value)
    {
        int type = (int) _type;
        dicstat[_type] += _value; //스탯에 추가
        DicStatBonus[_type] += _value;

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

    public float GetStatBonus(STAT_TYPE type)
    {
        return DicStatBonus[type];
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

    public void Print()
    {
        //Debug.Log("==== Stat Data ====");
        //foreach (var pair in dicstat)
        //{
        //    Debug.Log($"{pair.Key}: {pair.Value}");
        //}
        
    }

    public StatDataJson ToJsonData()
    {
        StatDataJson json = new StatDataJson();

        foreach (var pair in dicstat)
        {
            json.ListStat.Add(new StatDataJson.StatEntry { type = pair.Key, value = pair.Value });
        }
        return json;
    }

    public void ApplyJsonData(StatDataJson _json)
    {
        //dicstat.Clear();
        foreach(var entry in _json.ListStat)
        {
            dicstat[entry.type] = entry.value;
        }
    }

    public string GetStatColor(STAT_TYPE _type)
    {
        switch(_type)
        {
            case STAT_TYPE.ATK:
                return "B22222";
            case STAT_TYPE.DEF:
                return "4682B4";
            case STAT_TYPE.SPEED:
                return "2E8B57";
            default:
                return "FFFFFF";
        }
    }
}
