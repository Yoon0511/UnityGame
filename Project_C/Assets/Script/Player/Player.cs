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

    //아이템, 인벤토리 테스트
    public GameObject item;
    int itemcount = 0;
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

        if (Input.GetKeyDown(KeyCode.F3))
        {
            Item obj = Instantiate(item).GetComponent<Item>();
            obj.ItemId = itemcount++;
            inventory.AddItem(obj);
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

}