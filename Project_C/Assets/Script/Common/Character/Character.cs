using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character<T> : MonoBehaviour
{
    public StatData Statdata;
    protected string Character_name;
    public BuffMgr BuffMgr;

    private void Start()
    {
        Init();
    }
    public abstract void Init();
    public abstract void Hit(float _damage);
    public string GetCharacterName() { return Character_name; }
    public float GetInStatData(STAT_TYPE _type)
    { 
        return Statdata.GetData(_type); 
    }
    public StatData GetStatData()
    {
        return Statdata;
    }
}