using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract partial class Character : Object
{
    public StatData Statdata;
    public BuffSystem BuffSystem;
    [SerializeField]
    protected string CharacterName;
    [SerializeField]
    protected int CharacterType = (int)CHARACTER_TYPE.NONE;
    protected bool IsStun = false;
    protected Character TargetCharacter;
    [SerializeField]
    protected GameObject BodyParticlePoint;

    private void Start()
    {
        Init();
    }
    public abstract void Init();
    public abstract void Hit(DamageData _damagedata);
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

    public abstract void RayTargetEvent(Character _character);
    // RayCast�� ���õǾ����� ȣ��

    // Character�� ��ӹ޴� ��� Ŭ������ RayTargetEvent ����
    // ��ӹ޴� Ŭ�������� RayTargetEvent ����
    // ������ CharacterType�� �´� �ൿ ����

    public virtual void EnhanceStat(STAT_TYPE _type, float _num)
    {
        Statdata.EnhanceStat(_type, _num);
    }
    public virtual void AddBuff(Buff _buff)
    {
        BuffSystem.AddBuff(_buff);
    }

    public virtual void AddDeBuff(DeBuff _debuff)
    {
        BuffSystem.AddBuff(_debuff);
    }

    public void SetIsStun(bool _stun) { IsStun = _stun; }
    public bool GetIsStun() { return IsStun; }

    public void SetTargetCharacter(Character _target) { TargetCharacter = _target; }
    public Character GetTargetCharacter() { return TargetCharacter; }
    public GameObject GetBodyParticlePointObj() { return BodyParticlePoint; }
    
    public void LookatTarget()
    {
        transform.LookAt(GetTargetCharacter().transform, Vector3.up);
    }
}