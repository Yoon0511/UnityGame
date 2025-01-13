using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public partial class Player : Character<STATE>
{
    float walkspeed;
    float runspeed;

    Monster target;

    //������, �κ��丮 �׽�Ʈ
    public GameObject item;
    int itemcount = 0;

    public EquipmentItem weapon;
    public EquipmentItem amor;
    public EquipmentItem ring;
    //�׽�Ʈ
    
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
            ChangeState(STATE.ATTACK,(int)PLAYER_ANI_STATE.ATTACK);
        }

        //�׽�Ʈ
        if (Input.GetKeyDown(KeyCode.F3)) //����
        {
            Item obj = Instantiate(item).GetComponent<Item>();
            obj.Id = itemcount++;
            inventory.AddItem(obj);
        }

        if (Input.GetKeyDown(KeyCode.F5)) //����
        {
            inventory.AddItem(weapon);
            //equiment.EquippedItem(weapon);
        }
        if (Input.GetKeyDown(KeyCode.F6)) //����
        {
            inventory.AddItem(amor);
        }
        if (Input.GetKeyDown(KeyCode.F7)) //�Ǽ�����
        {
            inventory.AddItem(ring);
        }
    }

    public override void Init()
    {
        Character_name = "Player_1";
        Fsm_Init();
    }

    //Ű���� ����
    void UpdateAnimation()
    {
        if (CurrState == STATE.ATTACK)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space)) // ����
        {
            ChangeState(STATE.ATTACK);
        }
        else if (Input.GetKey(KeyCode.LeftShift)) // �޸���
        {
            //animator.SetInteger("Ani_State", (int)STATE.RUN);
            ChangeState(STATE.RUN);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift)) // �ȱ�
        {
            //animator.SetInteger("Ani_State", (int)STATE.WALK);
            ChangeState(STATE.WALK);
        }
    }

    void CheckHP()
    {
        if(Statdata.GetData(STAT_TYPE.HP) <= 0)
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


//�÷��̾�
//��ų����Ʈ - ��ų�Ŵ��� <- ��ųUI
//��񸮽�Ʈ <- ���â
//�κ��丮