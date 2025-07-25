using System.Collections;
using System.Collections.Generic;
using System.Data;
using ExitGames.Client.Photon.StructWrapping;
using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;

//Character를 상속받는 Monster
public partial class Monster : Character
{
    protected GameObject player;
    protected List<Player> ListPlayer;

    public bool OnPatrol = false;

    [SerializeField]
    protected PhotonView PV;
    protected override void FixedUpdate()
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
        AlignToTerrainHeight();
    }
    private void Awake()
    {
        PV = gameObject.GetComponent<PhotonView>();
    }
    // Monster Init - 몬스터 생성시 기본 세팅값 설정
    public override void Init()
    {
        CharacterName = gameObject.name.Replace("(Clone)", "").Trim();
        CharacterType = (int)CHARACTER_TYPE.MONSTER;
        Id = (int)MONSTER_ID.GOLEM;
        //player = Shared.GameMgr.PLAYEROBJ;
        transform.SetParent(Shared.GameMgr.Monsters.transform, true);
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

    // Monster가 Damage를 받을 시 실행되는 함수
    public override void Hit(DamageData _damagedata)
    {
        if(IsDead)
        {
            return;
        }
        Shake(0.1f, 0.1f);

        Shared.GameMgr.Hphud.ShowHud(this);
        PV.RPC("RpcMonsterTakeDamage", RpcTarget.All, (float)_damagedata.Damage, (int)_damagedata.DamageFont_Type);
        CheckHP();
    }

    [PunRPC]
    protected void RpcMonsterTakeDamage(float _damage,int _damagefontType)
    {
        DamageData damgedata = Shared.GameMgr.DamageDataPool.Get(_damage, (DAMAGEFONT_TYPE)_damagefontType);
        Statdata.TakeDamage(damgedata);
        PV.RPC("RpcSyncHp", RpcTarget.All, GetInStatData(STAT_TYPE.HP));
    }
    [PunRPC]
    protected  void RpcSyncHp(float _hp)
    {
        Statdata.SetStat(STAT_TYPE.HP, _hp);
    }

    protected virtual bool CheckHP() 
    {
        if(Statdata.GetData(STAT_TYPE.HP) <= 0)
        {
            SendQuestMsg(); //Monster -> Player 퀘스트 메시지 발송
            DropItem(); //전리품 드랍

            //사망
            //AllRemoveMapIcon();
            //Shared.GameMgr.RemoveMonster(this);
            //Destroy(gameObject);
            ChangeState((int)MONSTER_STATE.DIE);
            return true;
        }
        return false;
    }
    void DropItem()
    {
        Shared.GameMgr.PLAYER.AddExp(25.0f);
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
