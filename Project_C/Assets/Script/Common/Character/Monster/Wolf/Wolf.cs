using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wolf : Monster
{
    [SerializeField]
    bool IsLeader;

    public List<Wolf> ListColleagueMonster = new List<Wolf>();
    public override void Init()
    {
        CharacterName = gameObject.name;
        CharacterType = (int)CHARACTER_TYPE.MONSTER;
        Id = (int)MONSTER_ID.WOLF;
        player = Shared.GameMgr.PLAYEROBJ;
        transform.SetParent(Shared.GameMgr.Monsters.transform, true);
        Fsm_Init();

        if(IsLeader)
        {
            ListColleagueMonster.Add(this);

            for (int i = 0; i < ListColleagueMonster.Count - 1; ++i)
            {
                ListColleagueMonster[i].ListColleagueMonster = ListColleagueMonster;
            }
        }
    }

    public override void Hit(DamageData _damagedata)
    {
        base.Hit(_damagedata);

        //동료에게 알림
        if(GetCurrState() != (int)MONSTER_STATE.ATTACK &&
            GetCurrState() != (int)MONSTER_STATE.CHASE)
        {
            for (int i = 0; i < ListColleagueMonster.Count; ++i)
            {
                ListColleagueMonster[i].ChangeTarget(Shared.GameMgr.PLAYEROBJ);
                ListColleagueMonster[i].ChangeState((int)MONSTER_STATE.CHASE);
            }
        }
    }
    public override void Fsm_Init()
    {
        Fsm = new StateMachine(new Monster_IdleState(this));
        CurrState = (int)MONSTER_STATE.IDLE;
        PrevState = CurrState;

        DicState.Add((int)MONSTER_STATE.IDLE, new Monster_IdleState(this));
        DicState.Add((int)MONSTER_STATE.MOVE, new Monster_MoveState(this));
        DicState.Add((int)MONSTER_STATE.PATROL, new Monster_PatrolState(this));
        DicState.Add((int)MONSTER_STATE.CHASE, new Wolf_ChaseState(this)); //늑대 추격상태
        DicState.Add((int)MONSTER_STATE.ATTACK, new Monster_AttackState(this));
        DicState.Add((int)MONSTER_STATE.DIE, new Monster_DieState(this));

        Fsm.ChangeState(DicState[(int)MONSTER_STATE.IDLE]);

        PathNodeInit();
    }

    public void ChangeStateColleague(int _state)
    {
        for (int i = 0; i < ListColleagueMonster.Count; ++i)
        {
            ListColleagueMonster[i].ChangeState(_state);
        }
    }

    public bool GetIsLeader() { return IsLeader; }
    public void SetIsLeader(bool _value) { IsLeader = _value; }

    //탐지거리 및 공격사거리
    //public void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, detectionRange);
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}
