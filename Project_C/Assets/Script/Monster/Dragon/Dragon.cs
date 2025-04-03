using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Dragon : Monster
{
    private void FixedUpdate()
    {
        Fsm.UpdateState();
    }
    
    public override void Hit(float _damage)
    {
        Statdata.TakeDamage(_damage);
    }

    public override void Init()
    {
        SkillInit();
        Fsm_Init();
    }

    public override void RayTargetEvent()
    {
        
    }
}
