using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character<T> : MonoBehaviour
{
    public StatData statdata;
    protected string character_name;
    float test;

    private void Start()
    {
        Init();
    }
    public abstract void Init();
    public abstract void Hit(float _damage);
    public string GetCharacterName() { return character_name; }
    public float GetInStatData(STAT_TYPE _type)
    { 
        return statdata.GetData(_type); 
    }
    public StatData GetStatData()
    {
        return statdata;
    }
}