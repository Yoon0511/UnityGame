using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player
{
    [SerializeField]
    BuffUi BuffUi;

    public override void UseSkill(int _index)
    {
        UseSkill(_index,(int)STATE.ATTACK, CurrentSkill[_index].SkillMotion, CurrentSkill[_index].Instant);
        //UseSkill(_index,CurrentSkill[_index].SkillMotion);
    }
}