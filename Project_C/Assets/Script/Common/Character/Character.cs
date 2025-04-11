using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character : MonoBehaviour
{
    public StatData Statdata;
    public BuffSystem BuffSystem;
    protected string CharacterName;
    protected int CharacterType = (int)CHARACTER_TYPE.NONE;

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

    public int GetCharacterType() { return CharacterType; }

    public abstract void RayTargetEvent();
    // RayCast로 선택되었을때 호출

    // Character를 상속받는 모든 클래스에 RayTargetEvent 선언
    // 상속받는 클래스에서 RayTargetEvent 정의
    // 각각의 CharacterType에 맞는 행동 구현
}