using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Character를 상속받는 Player
public partial class Player : Character
{
    //아이템, 인벤토리 테스트
    public GameObject item;
    int itemcount = 0;

    public EquipmentItem weapon;
    public EquipmentItem amor;
    public EquipmentItem ring;
    //아이템, 인벤토리 테스트

    private void Awake()
    {
        Shared.GameMgr.PLAYER = this;
        Shared.GameMgr.PLAYEROBJ = this.gameObject;
    }
    private void FixedUpdate()
    {
        Fsm.UpdateState();
        //KeyboardMove();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 공격
        {
            //ChangeState((int)STATE.ATTACK,(int)PLAYER_ANI_STATE.ATTACK);
            OnAttack();
        }
        if(IsComboEnable)
        {
            OnAttack();
        }

        //가드 테스트
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Guard();
        }

        //테스트
        if (Input.GetKeyDown(KeyCode.F3)) //포션
        {
            ItemBase obj = Instantiate(item).GetComponent<ItemBase>();
            obj.Id = itemcount++;
            Inventory.AddItem(obj);
        }

        if (Input.GetKeyDown(KeyCode.F5)) //무기
        {
            Weapon obj = Instantiate(weapon).GetComponent<Weapon>();
            Inventory.AddItem(obj);
            //equiment.EquippedItem(weapon);
        }
        if (Input.GetKeyDown(KeyCode.F6)) //갑옷
        {
            Amor obj = Instantiate(amor).GetComponent<Amor>();
            Inventory.AddItem(obj);
        }
        if (Input.GetKeyDown(KeyCode.F7)) //악세서리
        {
            Accessories obj = Instantiate(ring).GetComponent<Accessories>();
            Inventory.AddItem(obj);
        }

        if (Input.GetKeyDown(KeyCode.F8)) //HP
        {
            Statdata.EnhanceStat(STAT_TYPE.HP, -100f);
            UpdateUnitFrame();
        }

        if (Input.GetKeyDown(KeyCode.F9)) //MP
        {
            //Statdata.EnhanceStat(STAT_TYPE.HP, 100f);
            //Statdata.EnhanceStat(STAT_TYPE.MP, -10f);
            AddExp(10f);
            //UpdateUnitFrame();
        }

        if(Input.GetMouseButtonDown(0))
        {
            ClickToRay();
        }

        if (Input.GetKeyDown(KeyCode.F10)) //퀘스트 테스트
        {
            //Debug.Log("hunting");
            HuntingMsg huntingMsg = new HuntingMsg();
            huntingMsg.SetMsg(10, 10, (int)QUEST_TYPE.HUNTING, (int)MONSTER_ID.GOLEM, 3);
            QusetProgress(huntingMsg);

            //DeBuff deBuff = new DeBuff_Stun(0.5f, gameObject, "UI_Skill_Icon_Blackhole");

            //도트데미지 테스트
            //DotDamage dot = new DotDamage(0.5f,STAT_TYPE.HP,30f,5f,gameObject, "UI_Skill_Icon_PsycicAttack");

            //AddBuff(dot);


            //골드 테스트
            AddGold(3000);
        }
    }

    public void ClickToRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
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

        Fsm_Init();
        UpdateUnitFrame();
        InventoryInit();
        StartCoroutine(AutomaticRecovery(1.0f));
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

    //키보드 조작
    void UpdateAnimation()
    {
        if (CurrState == (int)STATE.ATTACK)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // 공격
        {
            ChangeState((int)STATE.ATTACK);
        }
        else if (Input.GetKey(KeyCode.LeftShift)) // 달리기
        {
            //animator.SetInteger("Ani_State", (int)STATE.RUN);
            ChangeState((int)STATE.RUN);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) // 걷기
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
        // 가장 가까운 아이템 탐색 후 습득 - 인벤토리 적립
        
        // 필드 아이템 리스트 -> 검색 -> 습득
    }
}