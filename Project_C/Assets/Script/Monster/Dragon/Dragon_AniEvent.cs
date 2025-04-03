using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Dragon
{
    //public void OnAniStart()
    //{
    //
    //}
    //public void OnAniEnd()
    //{
    //    //ChangeState((int)DRAGON_STATE.IDLE);
    //}

    public void OnCurrentUseSkill()
    {
        CurrentUseSkill();
    }

    public void OnBreathEnd()
    {
        CurrentSkill[0].SkillEnd();
    }
}