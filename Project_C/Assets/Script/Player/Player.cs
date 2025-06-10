using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

//Character�� ��ӹ޴� Player
public partial class Player : Character
{
    //������, �κ��丮 �׽�Ʈ
    public GameObject item;
    int itemcount = 0;

    public EquipmentItem weapon;
    public EquipmentItem amor;
    public EquipmentItem ring;
    //������, �κ��丮 �׽�Ʈ

    //private void Awake()
    //{
    //    
    //}
    private void FixedUpdate()
    {
        Fsm.UpdateState();
        //KeyboardMove();
    }
    // Update is called once per frame
    void Update()
    {
        KeyInput();
    }

    public void ClickToRay()
    {
        PhotonViewIsMine();

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
        CharacterType = (int)CHARACTER_TYPE.PLAYER;
        CharacterName = "Player_1";

        for (int i = 0; i < MaxSkillCount; i++)
        {
            CurrentSkill.Add(null);
        }

        UiInit();
        Fsm_Init();
        UpdateUnitFrame();
        InventoryInit();
        //�ڵ�ȸ��
        StartCoroutine(AutomaticRecovery(1.0f));

        //PhotonViewIsMine();
        //Shared.GameMgr.PLAYER = this;
        //Shared.GameMgr.PLAYEROBJ = this.gameObject;

    }

    public override void RayTargetEvent(Character _character)
    {
        Shared.GameMgr.Hphud.SetTarget(this);
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

    public void UiInit()
    {
        PhotonViewIsMine();
        Shared.GameMgr.PLAYER = this;
        Shared.GameMgr.PLAYEROBJ = gameObject;

        Inventory = Shared.UiMgr.Inventory;
        BuffUi = Shared.UiMgr.BuffUi;
        UnitFrame = Shared.UiMgr.UnitFrame;

        //����Ʈ
        QUESTLISTUI = Shared.UiMgr.QuestListUi;
        QUESTLISTUI.SetPlayerQuest(GetProgressQusetList());

        //ī�޶� Ÿ��
        Shared.MainCamera.SetTarget(transform);

        //�̴ϸ�,�����
        Shared.UiMgr.MiniMap.Init(gameObject);
        Shared.UiMgr.WorldMap.Init(gameObject);

        //���̽�ƽ
        Shared.GameMgr.JOYSTICK.SetTarget(this);

        //��ų��
        Shared.UiMgr.SkillBook.Init(this);
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
}