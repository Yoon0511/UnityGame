using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;

public class DamageDataPool
{
    private Queue<DamageData> Pool = new Queue<DamageData> ();

    public DamageData Get(float _damage,DAMAGEFONT_TYPE _damagefont_type)
    {
        DamageData data = Pool.Count > 0 ? Pool.Dequeue() : new DamageData(_damage, _damagefont_type);
        data.Pool = this;
        data.Damage = _damage;
        data.DamageFont_Type = _damagefont_type;
        return data;
    }

    public void Return(DamageData _data)
    {
        Pool.Enqueue(_data);
    }
}
