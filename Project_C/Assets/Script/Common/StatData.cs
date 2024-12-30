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

    public void TakeDamage(float _damage)
    {
        float damage = _damage - dicstat[STAT_TYPE.DEF];
        Shared.GameMgr.CREATE_DAMAGE_TEXT.Create(uihead, Color.black, damage);
        dicstat[STAT_TYPE.HP] = dicstat[STAT_TYPE.HP] - damage;
    }

    public void EnhanceStat(STAT_TYPE _type,float _num)
    {
        dicstat[_type] += _num;
    }

    public float GetData(STAT_TYPE _type)
    {
        return dicstat[_type];
    }
}
