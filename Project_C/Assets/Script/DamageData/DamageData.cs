using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageData
{
    public int Damage;
    public DAMAGEFONT_TYPE DamageFont_Type;
    public DamageDataPool Pool;

    public DamageData(float _damage,DAMAGEFONT_TYPE _damagefont_type)
    {
        Damage = (int)_damage;
        DamageFont_Type = _damagefont_type;
    }

    public void Return()
    {
        Pool.Return(this);
    }
}
