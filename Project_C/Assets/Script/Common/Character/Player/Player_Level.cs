using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player : Character
{
    float CurrLevel = 1f;
    float PrevLevel = 1f;
    int StatPoint = 0;
    int MaxPoint = 0;
    public void AddExp(float _value)
    {
        PrevLevel = CurrLevel;
        Statdata.AddExp(_value);
        CurrLevel = Statdata.GetData(STAT_TYPE.LEVEL);

        UpdateExpbar();

        if(PrevLevel != CurrLevel)
        {
            LevelUp();
        }
    }

    void LevelUp()
    {
        Shared.SoundMgr.PlaySFX("LEVELUP");
        Shared.ParticleMgr.CreateParticle("LevelUp", transform, 1.2f);
        
        UpgradeStat(STAT_TYPE.MAXHP, 50);
        UpgradeStat(STAT_TYPE.MAXMP, 30);
        //UpgradeStat(STAT_TYPE.ATK, 5);
        //UpgradeStat(STAT_TYPE.DEF, 1);

        StatPoint += 3;
        MaxPoint = StatPoint;

        UpdateUnitFrame();
        Shared.UiMgr.InfoPlayer.Refresh();
    }

    void UpgradeStat(STAT_TYPE _statType, float _amount)
    {
        EnhanceStat(_statType, _amount);
    }

    public int GetStatPoint() { return StatPoint; }
    public bool AddStatPoint(int _value)
    {
        int prev = StatPoint;
        StatPoint = Mathf.Clamp(StatPoint + _value, 0, MaxPoint);
        return StatPoint != prev;
    }

    public bool MinusStatPoint(int _value)
    {
        int prev = StatPoint;
        StatPoint = Mathf.Clamp(StatPoint - _value, 0, MaxPoint);
        return StatPoint != prev;
    }

    public void StatComplete()
    {
        MaxPoint = StatPoint;
    }
}
