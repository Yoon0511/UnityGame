using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Monster : Character
{
    [SerializeField]
    Player Target;

    [SerializeField]
    GameObject Item;

    StateMachine fsm;

    MONSTER_STATE state;
    Dictionary<MONSTER_STATE, StateBase> dicState = new Dictionary<MONSTER_STATE, StateBase>();

    private void FixedUpdate()
    {
        fsm.UpdateState();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            fsm.ChangeState(dicState[MONSTER_STATE.IDLE]);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            fsm.ChangeState(dicState[MONSTER_STATE.MOVE]);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            fsm.ChangeState(dicState[MONSTER_STATE.ATTACK]);
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
        Speed = 5.5f;
        Curr_State = STATE.NONE;
        Prev_State = STATE.NONE;

        fsm = new StateMachine(new MonsterIdle());
        state = MONSTER_STATE.IDLE;

        dicState.Add(MONSTER_STATE.IDLE, new MonsterIdle());
        dicState.Add(MONSTER_STATE.MOVE, new MonsterMove());
        dicState.Add(MONSTER_STATE.ATTACK, new MonsterAttack());

        fsm.ChangeState(dicState[MONSTER_STATE.IDLE]);
    }

    void DoAttack(Player player)
    {
        player.Hit(Atk);
    }
    void Move() { }
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
        Instantiate(Item,transform.position,transform.rotation);
    }
}
