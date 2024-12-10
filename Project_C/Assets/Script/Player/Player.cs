using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
public partial class Player : Character
{
    float WalkSpeed;
    float RunSpeed;

    Monster Target;
    [SerializeField]
    Inventory inventory;
    
    //테스트
    public GameObject item;
    int itemcount = 0;
    //테스트

    private void FixedUpdate()
    {
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
            Mp -= 5.5f;
            UpdateMpbar();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Hp += 5.0f;
            UpdateHpbar();
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            Mp += 5.5f;
            UpdateMpbar();
        }

        if (Input.GetKeyDown(KeyCode.Space)) // 공격
        {
            PlayAni_Trigger("Ani_ATK");
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
        MaxHp = 100.0f;
        MaxMp = 100.0f;
        Hp = MaxHp;
        Mp = MaxMp;
        Atk = 50.0f;
        Def = 2.0f;
        Speed = 8.0f;
        WalkSpeed = 5.5f;
        RunSpeed = 10.0f;
        Curr_State = STATE.IDLE;
        Prev_State = STATE.NONE;

        animator = GetComponentInChildren<Animator>();
    }

    //키보드 조작
    void UpdateAnimation()
    {
        if (Curr_State == STATE.ATTACK)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // 공격
        {
            animator.SetTrigger("Ani_ATK");
            ChangeState(STATE.ATTACK);
        }
        else if (Input.GetKey(KeyCode.LeftShift)) // 달리기
        {
            Speed = RunSpeed;
            animator.SetInteger("Ani_State", (int)STATE.RUN);
            ChangeState(STATE.RUN);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) // 걷기
        {
            Speed = WalkSpeed;
            animator.SetInteger("Ani_State", (int)STATE.WALK);
            ChangeState(STATE.WALK);
        }
    }
    public void AniATKEnd()
    {
        ChangeState(Prev_State);
    }

    void DoAttack(Monster monster)
    {
        monster.Hit(Atk);
    }

    void CheckHP()
    {
        if(Hp <= 0)
        {
            ChangeState(STATE.DIE);
        }
    }
    void GetItem()
    {
        // 가장 가까운 아이템 탐색 후 습득 - 인벤토리 적립
        
        // 필드 아이템 리스트 -> 검색 -> 습득
    }

    void Buff(int buff_stat,BUFF_TYPE type)
    {
        switch(type)
        {
            case BUFF_TYPE.HP:
                break;
            case BUFF_TYPE.MP:
                break;
            case BUFF_TYPE.ATK:
                break;
            case BUFF_TYPE.DEF:
                break;
            case BUFF_TYPE.SPEED:
                break;
        }
    }
    void DeBuff(int debuff_stat,BUFF_TYPE type)
    {
        switch (type)
        {
            case BUFF_TYPE.HP:
                break;
            case BUFF_TYPE.MP:
                break;
            case BUFF_TYPE.ATK:
                break;
            case BUFF_TYPE.DEF:
                break;
            case BUFF_TYPE.SPEED:
                break;
        }
    }
}