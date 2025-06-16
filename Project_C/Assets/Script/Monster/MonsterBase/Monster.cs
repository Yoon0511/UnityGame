using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

//Character를 상속받는 Monster
public partial class Monster : Character
{
    [SerializeField]
    protected GameObject Target;
    protected GameObject player;
    List<Player> ListPlayer;

    [SerializeField]
    protected float detectionRange;
    [SerializeField]
    protected float attackRange;

    public bool OnPatrol = false;
    bool IsDead = false;

    [SerializeField]
    PhotonView PV;
    private void FixedUpdate()
    {
        if(ListPlayer == null)
        {
            //player = Shared.GameMgr.PLAYEROBJ;
            ListPlayer = Shared.GameMgr.GetListPLayer();
            return;
        }
        if(PV == null)
        {
            PV = gameObject.GetComponent<PhotonView>();
            return;
        }

        if(PV.IsMine == false)
        {
            return;
        }

        StateUpdate();
    }
    private void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();
    }
    // Monster Init - 몬스터 생성시 기본 세팅값 설정
    public override void Init()
    {
        CharacterName = gameObject.name;
        CharacterType = (int)CHARACTER_TYPE.MONSTER;
        Id = (int)MONSTER_ID.GOLEM;
        //player = Shared.GameMgr.PLAYEROBJ;
        
        Fsm_Init();
    }
    public void SetPlayer(GameObject _player)
    {
        player = _player;
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
        Shake(0.1f, 0.1f);
        
        if(PhotonNetwork.IsMasterClient)
        {
            Statdata.TakeDamage(_damagedata);
            PV.RPC("RpcMonsterTakeDamage", RpcTarget.Others, _damagedata.Damage, (int)_damagedata.DamageFont_Type);
        }
        else
        {
            //rpc로 HP동기화
            PV.RPC("RpcMonsterTakeDamage", RpcTarget.All, _damagedata.Damage, (int)_damagedata.DamageFont_Type);

        }
        //Statdata.TakeDamage(_damagedata);
        //PV.RPC("RpcMonsterTakeDamage", RpcTarget.All, _damagedata.Damage, (int)_damagedata.DamageFont_Type);
        CheckHP();
    }

    [PunRPC]
    void RpcMonsterTakeDamage(float _damage,int _damagefontType)
    {
        DamageData damgedata = Shared.GameMgr.DamageDataPool.Get(_damage, (DAMAGEFONT_TYPE)_damagefontType);
        Statdata.TakeDamage(damgedata);
        PV.RPC("RpcSyncHp", RpcTarget.All, GetInStatData(STAT_TYPE.HP));
    }
    [PunRPC]
    void RpcSyncHp(float _hp)
    {
        Statdata.SetStat(STAT_TYPE.HP, _hp);
    }

    protected bool CheckHP() 
    {
        if(Statdata.GetData(STAT_TYPE.HP) <= 0)
        {
            SendQuestMsg(); //Monster -> Player 퀘스트 메시지 발송
            DropItem(); //전리품 드랍

            //삭제
            AllRemoveMapIcon();
            Shared.GameMgr.RemoveMonster(this);
            Destroy(gameObject);
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

    public bool GetIsDead() { return IsDead; }
}
