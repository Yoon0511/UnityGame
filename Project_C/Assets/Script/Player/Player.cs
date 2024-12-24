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

    //������, �κ��丮 �׽�Ʈ
    public GameObject item;
    int itemcount = 0;
    //�׽�Ʈ

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
        character_name = "Player_1";
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
            animator.SetInteger("Ani_State", (int)STATE.RUN);
            ChangeState(STATE.RUN);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) // �ȱ�
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
        // ���� ����� ������ Ž�� �� ���� - �κ��丮 ����
        
        // �ʵ� ������ ����Ʈ -> �˻� -> ����
    }

}