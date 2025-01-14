using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public partial class Monster : Character<MONSTER_STATE>
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
        player = Shared.GameMgr.PLAYEROBJ;
        Fsm_Init();
    }

    public override void Hit(float _damage)
    {
        Statdata.TakeDamage(_damage);

        if(CheckHP())
        {
            DropItem();
            Destroy(gameObject);
        }
    }

    bool CheckHP() 
    {
        if(Statdata.GetData(STAT_TYPE.HP) <= 0)
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
