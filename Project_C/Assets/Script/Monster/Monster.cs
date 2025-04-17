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

    public bool OnPatrol = false;
    private void FixedUpdate()
    {
        StateUpdate();
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            ChangeState((int)MONSTER_STATE.IDLE);
        }
        if (Input.GetKeyDown(KeyCode.U))
        {
            ChangeState((int)MONSTER_STATE.MOVE);
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ChangeState((int)MONSTER_STATE.ATTACK);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeState((int)MONSTER_STATE.DIE);
        }
    }
    public override void Init()
    {
        CharacterName = "Monster";
        CharacterType = (int)CHARACTER_TYPE.MONSTER;
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
    public void ChangeTarget(GameObject _target)
    {
        Target = _target;
    }

    public override void UseSkill(int _index)
    {
        
    }

    public override void RayTargetEvent()
    {
        Debug.Log("Monster RayTargetEvent");
    }
}
