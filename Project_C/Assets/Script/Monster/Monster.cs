using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public partial class Monster : Character
{
    [SerializeField]
    GameObject Target;

    GameObject player;

    [SerializeField]
    float detectionRange;
    [SerializeField]
    float attackRange;

    public bool OnPatrol = false;
    private void FixedUpdate()
    {
        StateUpdate();
    }
    // Update is called once per frame

    public override void Init()
    {
        CharacterName = "Monster";
        CharacterType = (int)CHARACTER_TYPE.MONSTER;
        player = Shared.GameMgr.PLAYEROBJ;
        Fsm_Init();

        //test//
        //Test();
        //test//
    }

    void Test()
    {
        StatBuff buff_1 = new StatBuff(STAT_TYPE.HP, 0.0f, 10.0f, gameObject, "UI_Skill_Icon_Arrow_Barrage");
        StatBuff buff_2 = new StatBuff(STAT_TYPE.HP, 0.0f, 20.0f, gameObject, "UI_Skill_Icon_PsycicAttack");
        StatBuff buff_3 = new StatBuff(STAT_TYPE.HP, 0.0f, 30.0f, gameObject, "UI_Skill_Icon_Slide");
        StatBuff buff_4 = new StatBuff(STAT_TYPE.HP, 0.0f, 40.0f, gameObject, "UI_Skill_Icon_SpiritArrows");

        BuffSystem.AddBuff(buff_1);
        BuffSystem.AddBuff(buff_2);
        BuffSystem.AddBuff(buff_3);
        BuffSystem.AddBuff(buff_4);
    }

    public override void Hit(DamageData _damagedata)
    {
        Shake(0.2f, 0.05f);
        Statdata.TakeDamage(_damagedata);

        if(CheckHP())
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    bool CheckHP() 
    {
        if(Statdata.GetData(STAT_TYPE.HP) <= 0)
        {
            return true;
        }
        return false;
    }
    void DropItem()
    {
        
    }
    public void ChangeTarget(GameObject _target)
    {
        Target = _target;
    }

    public override void UseSkill(int _index)
    {
        
    }

    public override void RayTargetEvent(Character _character)
    {
        Shared.GameMgr.Hphud.SetTarget(this);
    }
}
