using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character : MonoBehaviour
{
    public StatData Statdata;
    public BuffSystem BuffSystem;
    protected string CharacterName;

    private void Start()
    {
        Init();
    }
    public abstract void Init();
    public abstract void Hit(float _damage);
    public string GetCharacterName() { return CharacterName; }
    public float GetInStatData(STAT_TYPE _type)
    { 
        return Statdata.GetData(_type); 
    }
    public StatData GetStatData()
    {
        return Statdata;
    }
}