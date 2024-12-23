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
    
    //테스트
    public GameObject item;
    int itemcount = 0;
    //테스트

    private void FixedUpdate()
    {
        fsm.UpdateState();
        //KeyboardMove();
    }
    // Update is called once per frame
    void Update()
    {
        //UpdateAnimation();
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Hit(5.5f);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            mp -= 5.5f;
            UpdateMpbar();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            mp += 5.0f;
            UpdateHpbar();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            mp += 5.5f;
            UpdateMpbar();
        }

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
        maxhp = 100.0f;
        maxmp = 100.0f;
        hp = maxhp;
        mp = maxmp;
        atk = 50.0f;
        def = 2.0f;
        speed = 8.0f;
        walkspeed = 5.5f;
        runspeed = 10.0f;

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
            speed = runspeed;
            animator.SetInteger("Ani_State", (int)STATE.RUN);
            ChangeState(STATE.RUN);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) // 걷기
        {
            speed = walkspeed;
            animator.SetInteger("Ani_State", (int)STATE.WALK);
            ChangeState(STATE.WALK);
        }
    }

    void CheckHP()
    {
        if(hp <= 0)
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