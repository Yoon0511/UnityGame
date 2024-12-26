using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

public class StatData : MonoBehaviour
{
    [SerializeField]
    private SerializedDictionary<STAT_TYPE, float> dicstat;

    public void TakeDamage(float _damage)
    {
        dicstat[STAT_TYPE.HP] = dicstat[STAT_TYPE.HP] + (dicstat[STAT_TYPE.DEF] - _damage);
    }

    public void EnhanceStat(STAT_TYPE _type,float _num)
    {
        Debug.Log(_num);
        dicstat[_type] += _num;
    }

    public float GetData(STAT_TYPE _type)
    {
        return dicstat[_type];
    }
}
