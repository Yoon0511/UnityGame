using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Character
{
    float walkspeed;
    float runspeed;

    Monster target;
    [SerializeField]
    Inventory inventory;
    [SerializeField]
    EquipmentWindow equiment;


    //아이템, 인벤토리 테스트
    public GameObject item;
    int itemcount = 0;

    public EquipmentItem weapon;
    public EquipmentItem amor;
    public EquipmentItem ring;
    //테스트

    private void Awake()
    {
        Shared.GameMgr.PLAYER = this.gameObject;
    }
    private void FixedUpdate()
    {
        fsm.UpdateState();
        //KeyboardMove();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 공격
        {
            ChangeState(STATE.ATTACK);
        }

        //테스트
        if (Input.GetKeyDown(KeyCode.F3)) //포션
        {
            Item obj = Instantiate(item).GetComponent<Item>();
            obj.id = itemcount++;
            inventory.AddItem(obj);
        }

        if (Input.GetKeyDown(KeyCode.F5)) //무기
        {
            inventory.AddItem(weapon);
            //equiment.EquippedItem(weapon);
        }
        if (Input.GetKeyDown(KeyCode.F6)) //갑옷
        {
            inventory.AddItem(amor);
        }
        if (Input.GetKeyDown(KeyCode.F7)) //악세서리
        {
            inventory.AddItem(ring);
        }
    }

    public override void Init()
    {
        character_name = "Player_1";
        Fsm_Init();
    }

    //키보드 조작
    void UpdateAnimation()
    {
        if (curr_state == STATE.ATTACK)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // 공격
        {
            ChangeState(STATE.ATTACK);
        }
        else if (Input.GetKey(KeyCode.LeftShift)) // 달리기
        {
            animator.SetInteger("Ani_State", (int)STATE.RUN);
            ChangeState(STATE.RUN);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) // 걷기
        {
            animator.SetInteger("Ani_State", (int)STATE.WALK);
            ChangeState(STATE.WALK);
        }
    }

    void CheckHP()
    {
        if(statdata.GetData(STAT_TYPE.HP) <= 0)
        {
            ChangeState(STATE.DIE);
        }
    }
    void GetItem()
    {
        // 가장 가까운 아이템 탐색 후 습득 - 인벤토리 적립
        
        // 필드 아이템 리스트 -> 검색 -> 습득
    }

    public Inventory GetInventory() { return inventory; }
    public EquipmentWindow GetEquipmentWindow() { return equiment; }

    public void ApplyEquipItem(EquipmentItem _equipmentItem,bool UnEquip = false)
    {
        for(int i = 1;i<(int)STAT_TYPE.ENUM_END;++i)
        {
            float statValue = 0.0f;
            bool IsInStat = _equipmentItem.dic_EquipmentItemStat.TryGetValue((STAT_TYPE)i, out statValue);

            if(IsInStat && UnEquip == false) //장비 스탯 추가
            {
                statdata.EnhanceStat((STAT_TYPE)i, statValue);
            }
            else if(IsInStat && UnEquip) //장비 스탯 해제
            {
                statdata.EnhanceStat((STAT_TYPE)i, -statValue);
            }
        }
    }
}