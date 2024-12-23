using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public partial class Monster : Character
{
    [SerializeField]
    GameObject Target;

    GameObject player;

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
            ChangeState(MONSTER_STATE.IDLE);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            ChangeState(MONSTER_STATE.MOVE);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeState(MONSTER_STATE.ATTACK);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeState(MONSTER_STATE.DIE);
        }
    }
    public override void Init()
    {
        maxhp = 100.0f;
        maxmp = 100.0f;
        hp = maxhp;
        mp = maxmp;
        atk = 10.0f;
        def = 2.0f;
        speed = 2.0f;

        player = Shared.GameMgr.PLAYER;
        Fsm_Init();
    }

    public override void Hit(float damage)
    {
        float value = hp + (def - damage);
        hp = value;

        Debug.Log("MONSTER HP - " + hp);

        if(CheckHP())
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    bool CheckHP() 
    {
        if(hp <= 0)
        {
            return true;
        }
        return false;
    }
    void DropItem()
    {
        
    }
    public void ChageTarget(GameObject _target)
    {
        Target = _target;
    }
}
