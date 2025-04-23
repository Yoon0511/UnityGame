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
        CharacterName = "Dragon";
        CharacterType = (int)CHARACTER_TYPE.MONSTER;
        SkillInit();
        Fsm_Init();

        //test//
        //Test();
        //test//
    }
    void Test()
    {
        StatBuff buff_1 = new StatBuff(STAT_TYPE.HP, 0.0f, 15.0f, gameObject, "UI_Skill_Icon_Buff");
        StatBuff buff_2 = new StatBuff(STAT_TYPE.HP, 0.0f, 25.0f, gameObject, "UI_Skill_Icon_Beam");
        StatBuff buff_3 = new StatBuff(STAT_TYPE.HP, 0.0f, 35.0f, gameObject, "UI_Skill_Icon_Claw");
        StatBuff buff_4 = new StatBuff(STAT_TYPE.HP, 0.0f, 45.0f, gameObject, "UI_Skill_Icon_Reflect");

        BuffSystem.AddBuff(buff_1);
        BuffSystem.AddBuff(buff_2);
        BuffSystem.AddBuff(buff_3);
        BuffSystem.AddBuff(buff_4);
    }
    public override void RayTargetEvent()
    {
        Shared.GameMgr.Hphud.SetTarget(this);
    }
}
