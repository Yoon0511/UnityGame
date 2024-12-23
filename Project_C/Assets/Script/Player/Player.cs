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
    
    //�׽�Ʈ
    public GameObject item;
    int itemcount = 0;
    //�׽�Ʈ

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

        if (Input.GetKeyDown(KeyCode.Space)) // ����
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

    //Ű���� ����
    void UpdateAnimation()
    {
        if (curr_state == STATE.ATTACK)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // ����
        {
            ChangeState(STATE.ATTACK);
        }
        else if (Input.GetKey(KeyCode.LeftShift)) // �޸���
        {
            speed = runspeed;
            animator.SetInteger("Ani_State", (int)STATE.RUN);
            ChangeState(STATE.RUN);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) // �ȱ�
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
        // ���� ����� ������ Ž�� �� ���� - �κ��丮 ����
        
        // �ʵ� ������ ����Ʈ -> �˻� -> ����
    }

}