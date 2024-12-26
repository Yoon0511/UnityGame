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


    //������, �κ��丮 �׽�Ʈ
    public GameObject item;
    int itemcount = 0;

    public EquipmentItem weapon;
    public EquipmentItem amor;
    public EquipmentItem ring;
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

        //�׽�Ʈ
        if (Input.GetKeyDown(KeyCode.F3)) //����
        {
            Item obj = Instantiate(item).GetComponent<Item>();
            obj.id = itemcount++;
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

    public Inventory GetInventory() { return inventory; }
    public EquipmentWindow GetEquipmentWindow() { return equiment; }

    public void ApplyEquipItem(EquipmentItem _equipmentItem,bool UnEquip = false)
    {
        for(int i = 1;i<(int)STAT_TYPE.ENUM_END;++i)
        {
            float statValue = 0.0f;
            bool IsInStat = _equipmentItem.dic_EquipmentItemStat.TryGetValue((STAT_TYPE)i, out statValue);

            if(IsInStat && UnEquip == false) //��� ���� �߰�
            {
                statdata.EnhanceStat((STAT_TYPE)i, statValue);
            }
            else if(IsInStat && UnEquip) //��� ���� ����
            {
                statdata.EnhanceStat((STAT_TYPE)i, -statValue);
            }
        }
    }
}