using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    float CurrLevel = 1f;
    float PrevLevel = 1f;
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
        Shared.ParticleMgr.CreateParticle("LevelUp", transform, 1.2f);
    }
}
