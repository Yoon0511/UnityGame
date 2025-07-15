using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

//Player,Monster에 상속되는 Character Class
public abstract partial class Character : Object
{
    public StatData Statdata;
    public BuffSystem BuffSystem;
    protected string CharacterName;
    protected int CharacterType = (int)CHARACTER_TYPE.NONE;
    protected bool IsStun = false;
    protected Character TargetCharacter;
    [SerializeField]
    protected GameObject BodyParticlePoint;

    [SerializeField]
    protected int Id;
    protected bool IsDead = false;
    private void Start()
    {
        Init();
        AddGameMgrList();
    }
    protected virtual void FixedUpdate() { }

    //케릭터 생성시 기본 세팅을 위한 Init();
    public abstract void Init();
    //케릭터 별로 Damage Hit처리를 위한 함수
    public abstract void Hit(DamageData _damagedata);
    // RayCast로 선택되었을때 호출
    // Character를 상속받는 모든 클래스에 RayTargetEvent 선언
    // 상속받는 클래스에서 RayTargetEvent 정의
    // 각각의 CharacterType에 맞는 행동 구현
    public abstract void RayTargetEvent(Character _character);
    public void SetCharacterName(string _name) { CharacterName = _name; }
    public virtual string GetCharacterName() { return CharacterName; }
    public float GetInStatData(STAT_TYPE _type)
    { 
        return Statdata.GetData(_type);
    }
    public string GetStatColor(STAT_TYPE _type)
    {
        return Statdata.GetStatColor(_type);
    }
    public StatData GetStatData()
    {
        return Statdata;
    }

    public int GetCharacterType() { return CharacterType; }
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
    public int GetId() { return Id; }

    public void AddGameMgrList()
    {
        switch((CHARACTER_TYPE)CharacterType)
        {
            case CHARACTER_TYPE.NPC:
                Shared.GameMgr.AddNPC(this);
                break;
            case CHARACTER_TYPE.MONSTER:
                Shared.GameMgr.AddMonster(this);
                break;
        }
    }

    protected void AlignToTerrainHeight()
    {
        float TerrainHegiht = Shared.GameMgr.GetTerrainHeight(transform.position);
        Vector3 pos = transform.position;
        pos.y = TerrainHegiht + 0.1f;
        transform.position = pos;
    }

    public bool GetIsDead() { return IsDead; }
    public void SetIsDead(bool _isDead) { IsDead = _isDead; }
}