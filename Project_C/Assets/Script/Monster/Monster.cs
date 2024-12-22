using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public partial class Monster : Character
{
    [SerializeField]
    GameObject Target;

    [SerializeField]
    float detectionRange;
    [SerializeField]
    float attackRange;
    private void FixedUpdate()
    {
        StateUpdate();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChageState(MONSTER_STATE.IDLE);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            ChageState(MONSTER_STATE.MOVE);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChageState(MONSTER_STATE.ATTACK);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChageState(MONSTER_STATE.DIE);
        }
    }
    public override void Init()
    {
        MaxHp = 100.0f;
        MaxMp = 100.0f;
        Hp = MaxHp;
        Mp = MaxMp;
        Atk = 10.0f;
        Def = 2.0f;
        Speed = 2.0f;
        Curr_State = STATE.NONE;
        Prev_State = STATE.NONE;

        Fsm_Init();
    }

    public override void Hit(float damage)
    {
        float value = Hp + (Def - damage);
        Hp = value;

        Debug.Log("MONSTER HP - " + Hp);

        if(CheckHP())
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    bool CheckHP() 
    {
        if(Hp <= 0)
        {
            return true;
        }
        return false;
    }
    void DropItem()
    {
        
    }
}
