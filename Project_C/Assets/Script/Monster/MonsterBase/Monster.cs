using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

//Character를 상속받는 Monster
public partial class Monster : Character
{
    [SerializeField]
    protected GameObject Target;
    protected GameObject player;

    [SerializeField]
    protected float detectionRange;
    [SerializeField]
    protected float attackRange;

    public bool OnPatrol = false;


    private void FixedUpdate()
    {
        StateUpdate();
    }

    // Monster Init - 몬스터 생성시 기본 세팅값 설정
    public override void Init()
    {
        CharacterName = gameObject.name;
        CharacterType = (int)CHARACTER_TYPE.MONSTER;
        Id = (int)MONSTER_ID.GOLEM;
        player = Shared.GameMgr.PLAYEROBJ;
        Fsm_Init();
    }
    // Monster 클릭 시 실행되는 함수
    public override void RayTargetEvent(Character _character)
    {
        Shared.GameMgr.Hphud.SetTarget(this);
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

    // Monster가 Damage를 받을 시 실행되는 함수
    public override void Hit(DamageData _damagedata)
    {
        Shake(0.1f, 0.05f);
        Statdata.TakeDamage(_damagedata);

        CheckHP();
    }


    protected bool CheckHP() 
    {
        if(Statdata.GetData(STAT_TYPE.HP) <= 0)
        {
            SendQuestMsg(); //Monster -> Player 퀘스트 메시지 발송
            DropItem(); //전리품 드랍
            Destroy(gameObject); //삭제
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

    protected void SendQuestMsg()
    {
        HuntingMsg huntingMsg = new HuntingMsg();
        huntingMsg.SetMsg(10, 10, (int)QUEST_TYPE.HUNTING, Id, 3);
        Shared.GameMgr.PLAYER.QusetProgress(huntingMsg);
    }

    public Transform GetUiHead()
    {
        return Statdata.GetUiHeadTransform();
    }
}
