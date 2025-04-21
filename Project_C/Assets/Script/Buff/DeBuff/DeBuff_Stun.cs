using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeBuff_Stun : DeBuff
{
    public DeBuff_Stun(float _duration, GameObject _target, string _buffIconName) : base(_duration, _target, _buffIconName)
    {
        DebuffType = DEBUFF_TYPE.STUN;
    }
    public override void ApplyBuff()
    {
        Target.GetComponent<Character>().SetIsStun(true);
    }

    public override void EndBuff()
    {
        Target.GetComponent<Character>().SetIsStun(false);
    }
}
