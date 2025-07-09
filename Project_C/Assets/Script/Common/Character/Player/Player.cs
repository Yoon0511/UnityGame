using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

//Character�� ��ӹ޴� Player
public partial class Player : Character
{
    bool IsDead;
    PhotonView PV;
    protected override void FixedUpdate()
    {
        StateUpdate();
        AlignToTerrainHeight();
        //KeyboardMove();
    }
    // Update is called once per frame
    void Update()
    {
        KeyInput();
        Debug.Log(CurrState);
    }

    public void ClickToRay()
    {
        if(PV.IsMine == false)
        {
            return;
        }

        if(Shared.UiMgr.GetIsOpenUi())
        {
            return;
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        // VillageColliderLayer(6��) ����
        int IgnoreLayerMask = 1 << 6;
        int LayerMask = ~IgnoreLayerMask;

        if (Physics.Raycast(ray, out hit,Mathf.Infinity,LayerMask))
        {
            Character hitcharacter = hit.transform.GetComponent<Character>();

            if (hitcharacter != null)
            {
                hitcharacter.RayTargetEvent(this);
                SetTargetCharacter(hitcharacter);
            }

            //if (hit.transform.gameObject.GetComponent<Character>() != null)
            //{
            //    Debug.Log(hit.transform.gameObject.GetComponent<Character>());
            //    hit.transform.gameObject.GetComponent<Character>().RayTargetEvent();
            //}
        }
    }
    public override void Init()
    {
        PV = GetComponent<PhotonView>();
        CharacterType = (int)CHARACTER_TYPE.PLAYER;
        CharacterName = "Player_" + GetPhotonViewId().ToString();
        name = CharacterName;

        for (int i = 0; i < GetSkillList().Count; i++)
        {
            GetSkillList()[i].InitId();
        }

        for (int i = 0; i < MaxSkillCount; i++)
        {
            CurrentSkill.Add(null);
        }

        LocalInit();
        Fsm_Init();
        UpdateUnitFrame();
        InventoryInit();

        //�ڵ�ȸ��
        StartCoroutine(AutomaticRecovery(1.0f));

        ListMonster = Shared.GameMgr.GetListMonster();

        //PhotonViewIsMine();
        //Shared.GameMgr.PLAYER = this;
        //Shared.GameMgr.PLAYEROBJ = this.gameObject;

        //�ڱ� �̸� ǥ��
        if(PV.IsMine)
        {
            //Shared.UiMgr.Text.text = CharacterName;
        }
    }

    public override void RayTargetEvent(Character _character)
    {
        //Shared.GameMgr.Hphud.SetTarget(this);

        //�ٸ� �÷��̾� Ŭ�� ��
        if(PV.IsMine == false)
        {
            int OtherPVId = ((Player)_character).GetPhotonViewId();
            SelectPlayerViewId = OtherPVId;
            Shared.UiMgr.CreateSelectUi(GetPhotonViewId(),this);
        }
    }


    public override void EnhanceStat(STAT_TYPE _type, float _num)
    {
        base.EnhanceStat(_type, _num);

        UpdateUnitFrame();
    }

    public override void AddBuff(Buff buff)
    {
        base.AddBuff(buff);

        Shared.GameMgr.BUFFUI.AddBuff(buff,this);
        //Shared.ParticleMgr.CreateParticle("Buff", transform, 1.5f);
    }

    public override void AddDeBuff(DeBuff _debuff)
    {
        base.AddDeBuff(_debuff);
        Shared.GameMgr.BUFFUI.AddBuff(_debuff,this);

        switch (_debuff.GetDebuffType())
        {
            case DEBUFF_TYPE.STUN:
                {
                    PlayAnimation("Ani_State", (int)PLAYER_ANI_STATE.IDLE);
                    Shared.ParticleMgr.CreateParticle("Stun", BodyParticlePoint.transform, _debuff.Duration);
                    break;
                }
        }
    }

    public void LocalInit()
    {
        Inventory = Shared.UiMgr.Inventory;
        BuffUi = Shared.UiMgr.BuffUi;
        UnitFrame = Shared.UiMgr.UnitFrame;
        QUESTLISTUI = Shared.UiMgr.QuestListUi;
        equiment = Shared.UiMgr.EquipmentWindow;

        Shared.GameMgr.AddPlayer(this);

        if (PV.IsMine == false) return;

        Shared.GameMgr.PLAYER = this;
        Shared.GameMgr.PLAYEROBJ = gameObject;

        //���� Ÿ��
        Shared.GameMgr.AllMonsterSetPlayer();

        //����Ʈ
        QUESTLISTUI.SetPlayerQuest(GetProgressQusetList());

        //��ų��
        Shared.UiMgr.SkillBook.Init(this);

        //�̴ϸ�,�����
        Shared.UiMgr.MiniMap.Init(gameObject);
        Shared.UiMgr.WorldMap.Init(gameObject);

        //���̽�ƽ
        Shared.GameMgr.JOYSTICK.SetTarget(this);
        //ī�޶� Ÿ��
        Shared.MainCamera.SetTarget(transform);

        //Ż��
        RidingInit();

        //���̵���
        //Shared.GameMgr.FadeInOut.FadeIn();

        //��ȯ ����Ʈ
        StartCoroutine(ISpwanEffect());

        //�ҷ����� �� ���嵥���� �ε�
        //bool SaveDataLoad = Shared.SceneMgr.GetIsDataLoad();
        //if(SaveDataLoad)
        //{
        //    Shared.GameMgr.Load();
        //}
    }

    IEnumerator ISpwanEffect()
    {
        yield return new WaitForSeconds(0.3f);
        //��ȯ����Ʈ
        GameObject Effect =  Shared.ParticleMgr.CreateParticle("SpwanEffect", transform, 3.0f);
        Effect.transform.SetParent(transform);

        Shared.SoundMgr.PlaySFX("LEVELUP");
    }

    public int GetPhotonViewId()
    {
        return PV.ViewID;
    }

    public bool GetPVIsMine()
    {
        return PV.IsMine;
    }
    //Ű���� ����
    void UpdateAnimation()
    {
        if (CurrState == (int)STATE.ATTACK)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // ����
        {
            ChangeState((int)STATE.ATTACK);
        }
        else if (Input.GetKey(KeyCode.LeftShift)) // �޸���
        {
            //animator.SetInteger("Ani_State", (int)STATE.RUN);
            ChangeState((int)STATE.RUN);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) // �ȱ�
        {
            //animator.SetInteger("Ani_State", (int)STATE.WALK);
            ChangeState((int)STATE.WALK);
        }
    }

    void CheckHP()
    {
        if(Statdata.GetData(STAT_TYPE.HP) <= 0)
        {
            ChangeState((int)STATE.DIE);
        }
    }
    void GetItem()
    {
        // ���� ����� ������ Ž�� �� ���� - �κ��丮 ����
        
        // �ʵ� ������ ����Ʈ -> �˻� -> ����
    }

    public void SetIsDead(bool _isDead)
    {
        IsDead = _isDead;
    }

    public bool GetIsDead()
    {
        return IsDead;
    }
}