using System.Collections;
using System.Collections.Generic;
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
        if (Input.GetKeyDown(KeyCode.Space)) // ����
        {
            //ChangeState((int)STATE.ATTACK,(int)PLAYER_ANI_STATE.ATTACK);
            OnAttack();
        }
        if(IsComboEnable)
        {
            OnAttack();
        }

        //���� �׽�Ʈ
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            Guard();
        }

        //�׽�Ʈ
        if (Input.GetKeyDown(KeyCode.F3)) //����
        {
            ItemBase obj = Instantiate(item).GetComponent<ItemBase>();
            obj.Id = itemcount++;
            Inventory.AddItem(obj);
        }

        if (Input.GetKeyDown(KeyCode.F5)) //����
        {
            Weapon obj = Instantiate(weapon).GetComponent<Weapon>();
            Inventory.AddItem(obj);
            //equiment.EquippedItem(weapon);
        }
        if (Input.GetKeyDown(KeyCode.F6)) //����
        {
            Amor obj = Instantiate(amor).GetComponent<Amor>();
            Inventory.AddItem(obj);
        }
        if (Input.GetKeyDown(KeyCode.F7)) //�Ǽ�����
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

        if (Input.GetKeyDown(KeyCode.F10)) //����Ʈ �׽�Ʈ
        {
            //Debug.Log("hunting");
            HuntingMsg huntingMsg = new HuntingMsg();
            huntingMsg.SetMsg(10, 10, (int)QUEST_TYPE.HUNTING, (int)MONSTER_ID.GOLEM, 3);
            QusetProgress(huntingMsg);

            //DeBuff deBuff = new DeBuff_Stun(0.5f, gameObject, "UI_Skill_Icon_Blackhole");

            //��Ʈ������ �׽�Ʈ
            //DotDamage dot = new DotDamage(0.5f,STAT_TYPE.HP,30f,5f,gameObject, "UI_Skill_Icon_PsycicAttack");

            //AddBuff(dot);


            //��� �׽�Ʈ
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